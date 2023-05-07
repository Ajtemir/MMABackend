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
                SellerEmail = entity.Favorites.FirstOrDefault()?.User.Email,
                Description = entity.Description,
                Price = entity.Price,
                CategoryId = entity.CategoryId,
                Images = entity.Photos.Select(x=>x.Path).ToList(),
                IsFavorite = entity.Favorites.FirstOrDefault() is not null,
                FavoriteCount = entity.Favorites.Count,
            };
        }
    }
}