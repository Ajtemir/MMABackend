using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MMABackend.DataAccessLayer.Extensions;

namespace MMABackend.Controllers
{
    public partial class ProductsController
    {
        [HttpGet]
        public ActionResult<GetProductByIdAsSellerResult> GetProductPropertiesByProductId([FromQuery]GetProductPropertiesByProductIdArgument argument) => Execute( () =>
        {
            var response = _uow.GetProductProperties(argument.ProductId);
            return response;
        });
    }

    public class GetProductPropertiesByProductIdArgument
    {
        public int ProductId { get; set; }
    }
}