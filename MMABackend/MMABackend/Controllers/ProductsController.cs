using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MMABackend.DataAccessLayer;
using MMABackend.DomainModels.Common;

namespace MMABackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly UnitOfWork _uow;
        public ProductsController(UnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public ActionResult<List<Product>> Index()
        {
            var entities = _uow.Products.ToList();
            return Ok(entities);
        }
        
        [HttpPost]
        public ActionResult<List<Product>> Add(Product entity)
        {
            _uow.Products.Add(entity);
            _uow.SaveChanges();
            return Ok(entity);
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