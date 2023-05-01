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
        public ActionResult<List<Product>> Index(string email = null, bool isNew = false)
        {
            var entities = _uow.Products
                .Include(x => x.Category)
                .Include(x => x.User)
                .Include(x => x.Photos)
                .Where(x=>x.User.Email == email || email == null);
            if(isNew) entities = entities.OrderBy(x=>x.CreatedDate);
            List<ReadProductViewModel> result = entities.Select(x=>(ReadProductViewModel)x).ToList();
            return Ok(result);
        }
        
        
        
        [HttpPost]
        [Authorize(AuthenticationSchemes = AccessTokenConfig.SchemeName)]
        public ActionResult<Product> Add(AddProductViewModel model)
        {
            var email = HttpContext.GetEmailFromContext();
            var user = _uow.GetUserByEmail(email);
            Product product = model;
            product.UserId = user.Id;
            _uow.Products.Add(product);
            _uow.SaveChanges();
            return Ok(product.Id);
        }
        
        [HttpPost]
        public ActionResult<Product> AddWithEmail(AddProductWithEmailViewModel model)
        {
            var user = _uow.GetUserByEmail(model.UserEmail);
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