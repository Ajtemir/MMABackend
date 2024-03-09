using System;
using System.Collections.Generic;
using System.Linq;

namespace MMABackend.ViewModels.Product
{
    public class ReadProductViewModel
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
        
        public static explicit operator ReadProductViewModel(DomainModels.Common.Product entity)
        {
            return new ReadProductViewModel
            {
                Id = entity.Id,
                UserId = entity.UserId,
                SellerEmail = entity.User?.Email,
                Description = entity.Description,
                Price = entity.Price,
                CategoryId = entity.CategoryId,
                CategoryName = entity.Category?.Name,
                Images = entity.Photos?.Select(x=> x.Path).ToList() ?? new List<string>(),
                CollectiveInfo = entity.GroupDiscountProduct == null 
                    ? null
                    : new CollectiveInfo(
                        currentBuyerCount: entity.GroupDiscountProduct.CurrentPurchasersCount,
                        groupDiscountPrice: entity.GroupDiscountProduct.GroupDiscountPrice,
                        minBuyerCount: entity.GroupDiscountProduct.BuyerMinAmount,
                        startDate: entity.GroupDiscountProduct.StartDate,
                        endDate: entity.GroupDiscountProduct.EndDate
                    ),
            };
        }
    }

    public class CollectiveInfo
    {
        public CollectiveInfo(int currentBuyerCount, decimal groupDiscountPrice, int minBuyerCount, DateTime startDate, DateTime endDate)
        {
            CurrentBuyerCount = currentBuyerCount;
            GroupDiscountPrice = groupDiscountPrice;
            MinBuyerCount = minBuyerCount;
            StartDate = startDate;
            EndDate = endDate;
        }

        public int MinBuyerCount { get; set; }
        public int CurrentBuyerCount { get; set; }
        public decimal GroupDiscountPrice { get; set; }
        public DateTime StartDate { get;  }
        public DateTime EndDate { get; set; }
    } 
}