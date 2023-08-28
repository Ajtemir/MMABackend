using System.Linq;
using MMABackend.ViewModels.Product;

namespace MMABackend.ViewModelResults.Common
{
    public class GetProductByIdResult : ReadProductViewModel
    {
        public bool IsFavorite { get; set; } = false;
        public int FavoriteCount { get; set; } = 0;
        public static implicit operator GetProductByIdResult(DomainModels.Common.Product entity)
        {
            return new GetProductByIdResult
            {
                Id = entity.Id,
                UserId = entity.UserId,
                SellerEmail = entity.User?.Email,
                Description = entity.Description,
                Price = entity.Price,
                CategoryId = entity.CategoryId,
                Images = entity.Photos.Select(x=>x.Path).ToList(),
                IsFavorite = entity.Favorites.FirstOrDefault() is not null,
                FavoriteCount = entity.Favorites.Count,
                CollectiveInfo = entity.CollectiveSoldProduct is null 
                    ? null
                    : new CollectiveInfo(
                        entity.CollectiveSoldProduct.CurrentPurchasersCount,
                        entity.CollectiveSoldProduct.CollectivePrice,
                        entity.CollectiveSoldProduct.BuyerMinAmount,
                        entity.CollectiveSoldProduct.StartDate,
                        entity.CollectiveSoldProduct.EndDate
                    )
            };
        }
    }
}