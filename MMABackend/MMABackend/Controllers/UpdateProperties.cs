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
            // _uow.RemoveWhere<ProductProperty>(x=>x.ProductId == productId);
            foreach (var property in argument.Properties)
            {
                switch (property.IsMultipleOrLiteralDefault)
                {
                    case true:
                        break;
                    case false:
                        if (property.CurrentSingleValue != null)
                        {
                            
                        }
                        break;
                    case null:
                        break;
                }
            }
        });
    }

    public class UpdatePropertiesArgument
    {
        public List<Property> Properties { get; set; }
    }
}