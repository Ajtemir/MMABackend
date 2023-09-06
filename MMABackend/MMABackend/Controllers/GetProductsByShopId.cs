using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMABackend.Helpers.Common;
using MMABackend.ViewModelResults.Common;
using MMABackend.ViewModels.Product;

namespace MMABackend.Controllers
{
    public partial class ProductsController
    {
        [HttpGet]
        public ActionResult<GetProductByIdResult> GetProductsByShopId([FromQuery] GetProductsByShopId arguments) => Execute(
            () =>
            {
                var products = _uow.Shops.Include(x => x.Products)
                    .FirstOrError(x => x.Id == arguments.ShopId).Products.Select(x => (ReadProductViewModel)x).ToList();
                return products;
            });
    }

    public class GetProductsByShopId
    {
        public int ShopId { get; set; }
    }
}