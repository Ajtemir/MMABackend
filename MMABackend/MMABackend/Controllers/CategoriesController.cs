using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MMABackend.CustomMiddlewares;
using MMABackend.DataAccessLayer;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;
using MMABackend.ViewModels.Common;
using MMABackend.ViewModels.Product;

namespace MMABackend.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    // [ServiceFilter(typeof(ValidationFilterAttribute))]
    public partial class CategoriesController : BaseController
    {
        private readonly UnitOfWork _uow;
        private readonly IWebHostEnvironment _appEnvironment;


        public CategoriesController(UnitOfWork uow, IWebHostEnvironment appEnvironment, ILogger<CategoriesController> logger) : base(logger)
        {
            _uow = uow;
            _appEnvironment = appEnvironment;
        }

        [HttpPost]
        public async Task<ActionResult<ReadCategoryViewModel>> Add(AddCategoryViewModel model)
        {
            Category entity = model;
            _uow.Categories.FirstOrError(x => x.Name == model.Name, 
                "Category with this name already exists");
            var extension = Path.GetExtension(model.Name)!;
            var fileName = Guid.NewGuid();
            const string folder = "/categories/";
            var path =  folder + fileName + extension;
            await using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create)) await model.Image.CopyToAsync(fileStream);
            entity.ImagePath = path;
            _uow.Categories.Add(entity);
            await _uow.SaveChangesAsync();
            return Ok((ReadCategoryViewModel)entity);
        }
        
        [HttpDelete]
        public ActionResult<Category> Delete(DeleteCategoryViewModel model)
        {
            var entity = _uow.Categories.FirstOrError(x => x.Id == model.DeletedCategoryId);
            _uow.Categories.Remove(entity);
            _uow.SaveChanges();
            return Ok();
        }
    }
}