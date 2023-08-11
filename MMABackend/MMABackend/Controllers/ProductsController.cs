using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MMABackend.Configurations.Users;
using MMABackend.DataAccessLayer;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;
using MMABackend.ViewModelResults.Common;
using MMABackend.ViewModels.Product;

namespace MMABackend.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProductsController : ControllerBase
    {
        private readonly UnitOfWork _uow;
        public ProductsController(UnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public ActionResult<GetProductByIdResult> GetById([FromQuery]GetByEmailViewModel model)
        {
            var user = _uow.GetUserByEmailOrError(model.Email);
            var query = _uow.Products
                .Include(x => x.Favorites.Where(f => f.UserId == user.Id)).ThenInclude(x => x.User);
            var product = _uow.Products
                .Include(x => x.Favorites.Where(f=>f.UserId==user.Id)).ThenInclude(x => x.User)
                .FirstOrDefault(x => x.Id == model.ProductId);
            return Ok((GetProductByIdResult)product);

        }

        [HttpGet]
        public ActionResult<List<Product>> Index(string email = null, bool isNew = false)
        {
            var entities = _uow.Products
                .Include(x => x.Category)
                .Include(x => x.User)
                .Include(x => x.Photos)
                .Where(x=>x.User.Email == email || email == null);
            if(isNew) entities = entities.OrderByDescending(x=>x.CreatedDate);
            List<ReadProductViewModel> result = entities.Select(x=>(ReadProductViewModel)x).ToList();
            return Ok(new Response{data = result});
        }

        class Response
        {
            public  List<ReadProductViewModel> data { get; set; }
        }
        
        
        [HttpPost]
        [Authorize(AuthenticationSchemes = AccessTokenConfig.SchemeName)]
        public ActionResult<Product> Add(AddProductViewModel model)
        {
            var email = HttpContext.GetEmailFromContext();
            var user = _uow.GetUserByEmailOrError(email);
            Product product = model;
            product.UserId = user.Id;
            _uow.Products.Add(product);
            _uow.SaveChanges();
            return Ok((ReadProductViewModel)product);
        }
        
        [HttpPost]
        public ActionResult<Product> AddWithEmail(AddProductWithEmailViewModel model)
        {
            var user = _uow.GetUserByEmailOrError(model.UserEmail);
            Product product = model;
            product.UserId = user.Id;
            _uow.Products.Add(product);
            _uow.SaveChanges();
            return Ok(product.Id);
        }
        
        [HttpPut]
        public ActionResult<List<Product>> Update(Product entity)
        {
            _uow.Products.Update(entity);
            _uow.SaveChanges();
            return Ok(entity);
        }
        
        [HttpDelete]
        public ActionResult<List<Product>> Delete(int entityId)
        {
            var entity = _uow.Products.FirstOrDefault(x=>x.Id == entityId);
            if (entity is not null)
            {
                _uow.Products.Remove(entity);
                _uow.SaveChanges();
            }
            return Ok();
        }
    }
}