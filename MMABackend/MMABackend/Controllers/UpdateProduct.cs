using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class ProductsController
    {
        [HttpPost]
        public ActionResult UpdateProduct([FromBody]UpdateProductArgument argument) => Execute(() =>
        {
            var product = _uow.Products.Include(x=>x.ProductProperties).FirstOrError(x=>x.Id == argument.ProductId);
            product.Title = argument.Title;
            product.Description = argument.Description;
            product.Price = argument.Price;
            foreach (var productProperty in product.ProductProperties)
            {
                _uow.Remove(productProperty);
            }
            foreach (var property in argument.Properties)
            {
                switch (property.IsMultipleOrLiteralDefault)
                {
                    case true:
                        _uow.ProductProperties.AddRange(property.CurrentMultiValues.Select(x=> new ProductProperty
                        {
                            ProductId = product.Id,
                            PropertyKeyId = property.Id,
                            PropertyValueId = x,
                        }));
                        break;
                    case false:
                        _uow.ProductProperties.Add(new ProductProperty
                        {
                            ProductId = product.Id,
                            PropertyKeyId = property.Id,
                            PropertyValueId = property.CurrentSingleValue,
                        });
                        break;
                    case null:
                        _uow.ProductProperties.Add(new ProductProperty
                        {
                            ProductId = product.Id,
                            PropertyKeyId = property.Id,
                            NumberValue = property.CurrentNumberValue,
                        });
                        break;
                }
            }
            _uow.SaveChanges();
        });
    }

    public class UpdateProductArgument
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public List<Property> Properties { get; set; }
        
    }
    
}