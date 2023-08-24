using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MMABackend.DataAccessLayer;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;
using MMABackend.ViewModels.Product;

namespace MMABackend.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public partial class FavoritesController : BaseController
    {
        private readonly UnitOfWork _uow;

        public FavoritesController(UnitOfWork uow, ILogger<FavoritesController> logger) : base(logger)
        {
            _uow = uow;
        }

        [HttpGet]
        public ActionResult SetFavorite([FromQuery]FavoriteViewModel model)
        {
            _uow.Favorites.Add(new Favorite
            {
                ProductId = model.ProductId,
                UserId = _uow.GetUserByEmailOrError(model.Email).Id,
            });
            _uow.SaveChanges();
            return Ok();
        }
        
        [HttpGet]
        public ActionResult UnsetFavorite([FromQuery]FavoriteViewModel model)
        {
            var userId  = _uow.GetUserIdByEmailOrError(model.Email);
            var favorite = _uow.Favorites
                .FirstOrDefault(x=>x.ProductId == model.ProductId && x.UserId == userId) ??
                           throw new Exception("There is no such relation");
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

    
}