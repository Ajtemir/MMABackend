using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MMABackend.DataAccessLayer;
using MMABackend.DomainModels.Common;
using MMABackend.ViewModels.Common;
using MMABackend.ViewModels.Product;

namespace MMABackend.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CategoriesController : ControllerBase
    {
        private readonly UnitOfWork _uow;

        
        public CategoriesController(UnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public ActionResult<List<ReadCategoryViewModel>> Index()
        {
            var categories = _uow.Categories.Select(x=>(ReadCategoryViewModel)x).ToList();
            return Ok(categories);
        }

        [HttpPost]
        public ActionResult<ReadCategoryViewModel> Add(AddCategoryViewModel model)
        {
            Category entity = model;
            _uow.Categories.Add(entity);
            _uow.SaveChanges();
            return Ok((ReadCategoryViewModel)entity);
        }
        
        [HttpDelete]
        public ActionResult<Category> Delete(DeleteCategoryViewModel model)
        {
            var entity = _uow.Categories.FirstOrDefault(x => x.Id == model.DeletedCategoryId)
                ?? throw new Exception("Not Found");
            _uow.Categories.Remove(entity);
            _uow.SaveChanges();
            return Ok();
        }
    }
}