using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMABackend.Helpers.Common;
using MMABackend.ViewModels.Product;

namespace MMABackend.Controllers
{
    public partial class FavoritesController
    {
        [HttpGet]
        public ActionResult<List<ReadProductViewModel>> GetFavorites([FromQuery] GetAllFavoritesViewModel model) => Execute(() =>
            {
                var userId = _uow.GetUserIdByEmailOrError(model.Email);
                var products = _uow.Favorites.Include(x => x.Product)
                    .ThenInclude(x=>x.Photos)
                    .Include(x => x.User)
                    .Where(x => x.UserId == userId).Select(x => (ReadProductViewModel)x.Product).ToList();
                return products;
            }
        );
    }
    
    public class GetAllFavoritesViewModel
    {
        public string Email { get; set; }
    }
}