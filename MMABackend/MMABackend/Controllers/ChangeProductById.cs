using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;
using MMABackend.ViewModels.Product;

namespace MMABackend.Controllers
{
    public partial class ProductsController
    {
        [HttpGet]
        public ActionResult<List<Product>> GetChangeViewById([FromQuery]Argument model) => Execute(() =>
        {
            var product = _uow.Products
                .Include(x=>x.ProductProperties)
                .ThenInclude(x=>x.PropertyValue)
                .FirstOrError(x => x.Id == model.ProductId);
            
            var category = _uow.GetCategoryPropertyAndValuesById(product.CategoryId);

            IEnumerable<CategoryProperty> options = from prKey in category.PropertyKeys
                from prProperty in product.ProductProperties.Where(x => x.PropertyValue.PropertyKeyId == prKey.Id)
                    .DefaultIfEmpty()
                select new CategoryProperty
                {
                    Id = prProperty?.Id,
                    Name = prProperty?.PropertyValue.Name,
                    Options = prKey.PropertyValues.Select(x => new CategoryPropertyValueOptions
                    {
                        Id = x.Id,
                        Name = x.Name,
                    }).ToList(),
                };
                
            return new ProductEditViewModel
            {
                CategoryProperties = options,
                Product = (ReadProductViewModel)product,
            };
        });
    }

    public class Argument
    {
        public int ProductId { get; set; }
    }
    
    public class ProductEditViewModel
    {
        public ReadProductViewModel Product { get; set; }
        public IEnumerable<CategoryProperty> CategoryProperties { get; set; } = new List<CategoryProperty>();
    }

    public class CategoryProperty
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<CategoryPropertyValueOptions> Options { get; set; } = new List<CategoryPropertyValueOptions>();
    }
    
    public class CategoryPropertyValueOptions
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}