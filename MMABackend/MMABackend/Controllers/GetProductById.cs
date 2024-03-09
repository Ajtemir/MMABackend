using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;
using MMABackend.ViewModelResults.Common;

namespace MMABackend.Controllers
{
    public partial class ProductsController
    {
        [HttpGet]
        public ActionResult<GetProductByIdResult> GetById([FromQuery] GetByEmailArgument model) => Execute(() =>
        {
            var user = _uow.GetUserByEmailOrError(model.Email);
            var product = _uow.Products
                .Include(x=>x.AuctionProducts)
                .ThenInclude(x=>x.AuctionProductsUsers)
                .Include(x=>x.User)
                .Include(x=>x.Photos)
                .Include(x=> x.CollectiveSoldProducts.Where(p=>p.IsActual.Value))
                .ThenInclude(x=> x.CollectivePurchasers)
                .Include(x => x.Favorites.Where(f=> f.UserId == user.Id)).ThenInclude(x => x.User)
                .Include(x=>x.ProductProperties).ThenInclude(x=>x.PropertyKey).ThenInclude(x=>x.PropertyValues)
                .Include(x=>x.Category)
                .FirstOrError(x => x.Id == model.ProductId);
            
            var sellerCannotMakeCollectiveOwnProduct = (bool?)null;
            var isSeller = product.IsSeller(user);
            
            var isVoted = isSeller
                ? sellerCannotMakeCollectiveOwnProduct
                : product.GroupDiscountProduct?.CollectivePurchasers?.Exists(x => x.BuyerId == user.Id);

            AuctionState auctionState = isSeller
                ? product.AuctionProduct == null
                    ? AuctionState.SellerProductNotAuctioned
                    : AuctionState.SellerProductAuctioned
                : product.AuctionProduct == null 
                    ? AuctionState.BuyerProductNotAuctioned
                    : product.AuctionProduct.AuctionProductsUsers.FirstOrDefault(x=>x.UserId == user.Id) == null
                        ? AuctionState.BuyerNotApplied
                        : AuctionState.BuyerApplied;

            var properties = product.ProductProperties.GroupBy(x => x.PropertyKey)
            .Select(group =>
            {
                var keyProperty = new Property
                {
                    Id = group.Key.Id,
                    IsMultipleOrLiteralDefault = group.Key.IsMultipleOrLiteralDefault,
                    PropertyValues = group.Key.PropertyValues,
                };
                switch (group.Key.IsMultipleOrLiteralDefault)
                {
                    case true:
                        foreach (var productProperty in group)
                        {
                            if (productProperty?.PropertyValueId != null)
                            {
                                keyProperty.CurrentMultiValues.Add(productProperty.PropertyValueId.Value);
                            }
                        }
                        break;
                    
                    case false:
                        keyProperty.CurrentSingleValue = group.FirstOrDefault()?.PropertyValueId;
                        break;
                    
                    case null:
                        keyProperty.CurrentNumberValue = group.FirstOrDefault()?.NumberValue;
                        break;
                }
                return keyProperty;
            }).ToList();
            
            return GetByIdResult.Instance(product, isVoted, isSeller, auctionState, properties);
        });
    }

    
    
    public class GetByEmailArgument
    {
        public int ProductId { get; set; }
        public string Email { get; set; }
    }

    public class GetByIdResult
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string UserId { get; set; }
        public string SellerEmail { get; set; }
        public List<string> Images { get; set; }
        public CollectiveInfo CollectiveInfo { get; set; }
        public bool? IsSetCollective { get; set; } 
        public int FavoriteCount { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsSeller { get; set; } = false;
        public AuctionState AuctionState { get; set; }
        public AuctionDetail AuctionDetail { get; set; }
        public List<Property> Properties { get; set; }


        public static GetByIdResult Instance(Product product, bool? isVoted = null, bool isSeller = false, AuctionState state = default, List<Property> properties = null)
        {
            var casted = (GetByIdResult)product;
            casted.IsSetCollective = isVoted;
            casted.IsSeller = isSeller;
            casted.AuctionState = state;
            casted.Properties = properties;
            return casted;
        }

        public static explicit operator GetByIdResult(Product entity)
        {
            return new GetByIdResult
            {
                Id = entity.Id,
                UserId = entity.UserId,
                SellerEmail = entity.User?.Email,
                Description = entity.Description,
                Price = entity.Price,
                CategoryId = entity.CategoryId,
                CategoryName = entity.Category?.Name,
                Images = entity.Photos?.Select(x=> x.Path).ToList() ?? new List<string>(),
                IsFavorite = entity.Favorites.FirstOrDefault() is not null,
                FavoriteCount = entity.Favorites.Count,
                CollectiveInfo = entity.GroupDiscountProduct == null 
                    ? null
                    : new CollectiveInfo(
                        entity.GroupDiscountProduct.CurrentPurchasersCount,
                        entity.GroupDiscountProduct.GroupDiscountPrice,
                        entity.GroupDiscountProduct.BuyerMinAmount,
                        entity.GroupDiscountProduct.StartDate,
                        entity.GroupDiscountProduct.EndDate
                    ),
                AuctionDetail =entity.AuctionProduct == null 
                    ? null 
                    : new AuctionDetail
                    {
                        EndDate = entity.AuctionProduct.StartDate,
                        StartDate = entity.AuctionProduct.EndDate,
                        StartPrice = entity.AuctionProduct.StartPrice,
                        CurrentMaxPrice = entity.AuctionProduct.MaxPricedAuctionProductUser?.Price,
                    },
            };
        }
    }

    public class AuctionDetail
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal StartPrice { get; set; }
        public decimal? CurrentMaxPrice { get; set; }
        public decimal? CurrentMinPrice { get; set; }
    }

    public record CollectiveInfo(
        int CurrentBuyerCount,
        decimal GroupDiscountPrice,
        int MinBuyerCount,
        DateTime StartDate,
        DateTime EndDate
    );
}