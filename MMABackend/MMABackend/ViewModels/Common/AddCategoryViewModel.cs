using Microsoft.AspNetCore.Http;
using MMABackend.DomainModels.Common;

namespace MMABackend.ViewModels.Common
{
    public class AddCategoryViewModel
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }

        public static implicit operator Category(AddCategoryViewModel model)
        {
            return new Category
            {
                Name = model.Name,
            };
        }
    }
}