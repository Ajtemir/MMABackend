using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
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
    public partial class ProductsController : BaseController
    {
        private readonly UnitOfWork _uow;
        public ProductsController(UnitOfWork uow, ILogger<ProductsController> logger) : base(logger)
        {
            _uow = uow;
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
            var user = _uow.GetUserByEmailOrError(model.UserEmail ?? model.UserId);
            Product product = model;
            product.UserId = user.Id;
            _uow.Products.Add(product);
            _uow.SaveChanges();
            return Ok(Result.Ok((ReadProductViewModel)product));
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