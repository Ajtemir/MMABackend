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
        public ActionResult<GetProductByIdResult> GetById([FromQuery] GetByEmailViewModel model) => Execute(() =>
        {
            var user = _uow.GetUserByEmailOrError(model.Email);
            var query = _uow.Products
                .Include(x => x.Favorites.Where(f => f.UserId == user.Id)).ThenInclude(x => x.User);
            var product = _uow.Products
                .Include(x => x.Favorites.Where(f=>f.UserId==user.Id)).ThenInclude(x => x.User)
                .FirstOrDefault(x => x.Id == model.ProductId);
            return (GetProductByIdResult)product;
        });
    }
    
    public class GetByEmailViewModel
    {
        public int ProductId { get; set; }
        public string Email { get; set; }
    }
}