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
                .Include(x=>x.Photos)
                .FirstOrError(x => x.Id == model.ProductId);
            
            var category = _uow.GetCategoryPropertyAndValuesById(product.CategoryId);

            IEnumerable<CategoryProperty> options = from prKey in category.CategoryPropertyKeys
                from prProperty in product.ProductProperties.Where(x => x.PropertyValue.PropertyKeyId == prKey.Id)
                    .DefaultIfEmpty()
                select new CategoryProperty
                {
                    SelectedId = prProperty?.Id,
                    SelectedName = prProperty?.PropertyValue.Name,
                    PropertyKey = prKey.PropertyKey,
                };

            var res = options.GroupBy(x => x.PropertyKey.Id).Select(group => new
            {
                SelectedValueIds = group.Select(x => new
                {
                    x.SelectedId,
                    x.SelectedName,
                }),
                IsMultiple = group.First().PropertyKey.IsMultipleOrLiteralDefault,
                Options = group.First().PropertyKey.PropertyValues.Select(x => new
                {
                    x.Id,
                    x.Name,
                }),
                
            });
            return res;
            // return new ProductEditViewModel
            // {
            //     CategoryProperties = options,
            //     Product = (ReadProductViewModel)product,
            //     CategoryInfo = new CategoryInfo(product.Category.Id, product.Category.Name),
            // };
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
        public CategoryInfo CategoryInfo { get; set; }
    }

    public class CategoryProperty
    {
        public int? SelectedId { get; set; }
        public string SelectedName { get; set; }
        public IEnumerable<CategoryPropertyValueOptions> Options { get; set; } = new List<CategoryPropertyValueOptions>();
        public PropertyKey PropertyKey { get; set; }
    }

    public record CategoryInfo(int Id, string Name);
    
    public class CategoryPropertyValueOptions
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}