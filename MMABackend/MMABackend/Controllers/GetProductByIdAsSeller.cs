using Microsoft.AspNetCore.Mvc;

namespace MMABackend.Controllers
{
    public partial class ProductsController
    {
        [HttpGet]
        public ActionResult<GetProductByIdAsSellerResult> GetProductByIdAsSeller(GetProductByIdAsSellerArgument argument) => Execute(() =>
        {
            
        });
    }

    public class GetProductByIdAsSellerResult
    {
    }

    public class GetProductByIdAsSellerArgument
    {
    }
}