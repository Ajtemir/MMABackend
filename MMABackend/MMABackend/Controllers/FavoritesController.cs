using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMABackend.DataAccessLayer;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public class FavoritesController : ControllerBase
    {
        private readonly UnitOfWork _uow;

        public FavoritesController(UnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public ActionResult<List<Product>> Index(GetAllFavoritesViewModel model)
        {
            var userId  = _uow.GetUserIdByEmailOrError(model.Email);
            var products = _uow.Favorites.Include(x => x.Product)
                .Where(x => x.UserId == userId).Select(x => x.Product).ToList();
            return Ok(products);
        }

        [HttpGet]
        public ActionResult SetFavorite(FavoriteViewModel model)
        {
            _uow.Favorites.Add(new Favorite
            {
                ProductId = model.ProductId,
                UserId = _uow.GetUserByEmail(model.Email).Id,
            });
            _uow.SaveChanges();
            return Ok();
        }
        
        [HttpGet]
        public ActionResult UnsetFavorite(FavoriteViewModel model)
        {
            var userId  = _uow.GetUserIdByEmailOrError(model.Email);
            var favorite = _uow.Favorites.FirstOrError(x=>x.ProductId == model.ProductId && x.UserId == userId);
            _uow.Favorites.Remove(favorite);
            _uow.SaveChanges();
            return Ok();
        }
    }

    public class FavoriteViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public int ProductId { get; set; }
    }

    public class GetAllFavoritesViewModel
    {
        public string Email { get; set; }
    }
}