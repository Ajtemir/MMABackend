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
                .Include(x=>x.User)
                .Include(x=>x.Photos)
                .Include(x=> x.CollectiveSoldProducts.Where(c => c.IsActual.Value))
                .ThenInclude(x=>x.CollectivePurchasers)
                .Include(x => x.Favorites.Where(f=> f.UserId == user.Id)).ThenInclude(x => x.User)
                .FirstOrError(x => x.Id == model.ProductId);
            var isVoted = product.CollectiveSoldProduct.CollectivePurchasers?.Select(x => x.BuyerId).Contains(user.Id);
            return GetByIdResult.Instance(product, isVoted);
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
        public bool? IsVoted { get; set; } 
        
        public int FavoriteCount { get; set; }

        public bool IsFavorite { get; set; }


        public static GetByIdResult Instance(Product product, bool? isVoted)
        {
            var casted = (GetByIdResult)product;
            casted.IsVoted = isVoted;
            return casted;
        }


        public static explicit operator GetByIdResult(DomainModels.Common.Product entity)
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
                CollectiveInfo = entity.CollectiveSoldProduct == null 
                    ? null
                    : new CollectiveInfo(
                        entity.CollectiveSoldProduct.CurrentPurchasersCount,
                        entity.CollectiveSoldProduct.CollectivePrice,
                        entity.CollectiveSoldProduct.BuyerMinAmount,
                        entity.CollectiveSoldProduct.StartDate,
                        entity.CollectiveSoldProduct.EndDate
                    ),
            };
        }
    }

    public record CollectiveInfo(
        int CurrentBuyerCount,
        decimal DiscountedPrice,
        int MinBuyerCount,
        DateTime StartDate,
        DateTime EndDate
    );
}