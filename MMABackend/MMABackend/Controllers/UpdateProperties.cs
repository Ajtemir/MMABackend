using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MMABackend.DataAccessLayer.Extensions;
using MMABackend.DomainModels.Common;

namespace MMABackend.Controllers
{
    public partial class ProductsController
    {
        [HttpPost("{productId:int}")]
        public ActionResult UpdateProperties(int productId, [FromBody] UpdatePropertiesArgument argument) => Execute(() =>
        {
            var existProperties = _uow.ProductProperties.Where(x => x.ProductId == productId).ToList();
            _uow.RemoveRange(existProperties);
            _uow.SaveChanges();
            var productProperties = new List<ProductProperty>();
            foreach (var property in argument.Properties)
            {
                switch (property.IsMultipleOrLiteralDefault)
                {
                    case true:
                        productProperties.AddRange(property.CurrentMultiValues.Select(x=> new ProductProperty
                        {
                            ProductId = productId,
                            PropertyKeyId = property.Id,
                            PropertyValueId = x,
                        }));
                        break;
                    case false:
                        if (property.CurrentSingleValue != null)
                        {
                            productProperties.Add(new ProductProperty
                            {
                                ProductId = productId,
                                PropertyKeyId = property.Id,
                                PropertyValueId = property.CurrentSingleValue,
                            });
                        }
                        break;
                    case null:
                        if (property.CurrentNumberValue != null)
                        {
                            productProperties.Add(new ProductProperty
                            {
                                ProductId = productId,
                                PropertyKeyId = property.Id,
                                NumberValue = property.CurrentNumberValue,
                            });
                        }
                        break;
                }
            }
            _uow.AddRange(productProperties);
            _uow.SaveChanges();
        });
    }

    public class UpdatePropertiesArgument
    {
        public List<Property> Properties { get; set; }
    }
}