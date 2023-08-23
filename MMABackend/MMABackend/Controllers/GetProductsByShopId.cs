using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMABackend.Helpers.Common;
using MMABackend.ViewModelResults.Common;

namespace MMABackend.Controllers
{
    public partial class ProductsController
    {
        [HttpGet("{shopId:int}")]
        public ActionResult<GetProductByIdResult> GetProductsByShopId([FromQuery] GetProductsByShopId arguments, int shopId) => Execute(
            () =>
            {
                var products = _uow.Shops.Include(x => x.Products)
                    .FirstOrError(x => x.Id == shopId).Products.ToList();
                return products;
            });
    }

    public class GetProductsByShopId
    {
        
    }
}