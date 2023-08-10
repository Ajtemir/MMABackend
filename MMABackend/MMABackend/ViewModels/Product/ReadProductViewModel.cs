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
        public string UserId { get; set; }
        public string SellerEmail { get; set; }
        public List<string> Images { get; set; }
        
        public static implicit operator ReadProductViewModel(DomainModels.Common.Product entity)
        {
            return new ReadProductViewModel
            {
                Id = entity.Id,
                UserId = entity.UserId,
                SellerEmail = entity.User?.Email,
                Description = entity.Description,
                Price = entity.Price,
                CategoryId = entity.CategoryId,
                Images = entity.Photos?.Select(x=>x.Path).ToList() ?? new List<string>(),
            };
        }
    }
}