using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MMABackend.DataAccessLayer;
using MMABackend.DomainModels.Common;
using MMABackend.ViewModels.Common;

namespace MMABackend.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProductImageController : ControllerBase
    {
        private readonly UnitOfWork _uow;
        private readonly IWebHostEnvironment _appEnvironment;

        public ProductImageController(UnitOfWork uow, IWebHostEnvironment appEnvironment)
        {
            _uow = uow;
            _appEnvironment = appEnvironment;
        }
        
        [HttpDelete]
        public async Task<ActionResult> DeleteImagesByProductId(DeleteImagesByProductIdViewModel model)
        {
            var images = _uow.ProductPhotos.Where(x => x.ProductId == model.ProductId).ToList();
            _uow.ProductPhotos.RemoveRange(images);
            await _uow.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteImagesById([FromBody]List<DeleteImageViewModel> model)
        {
            var list = model.Select(x => x.DeletedImageId);
            var images = _uow.ProductPhotos
                .Where(x=> list.Contains(x.Id))
                .ToList();
            _uow.ProductPhotos.RemoveRange(images);
            await _uow.SaveChangesAsync();
            return Ok();
        }
        
        
        [HttpPost]
        public async Task<IActionResult> AddImage([FromForm]AddProductImageViewModel model)
        {
            foreach (var uploadedFile in model.Images)
            {
                var extension = Path.GetExtension(uploadedFile.FileName)!;
                var fileName = Guid.NewGuid();
                const string folder = "/images/";
                string path =  folder + fileName + extension;
                await using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    await uploadedFile.CopyToAsync(fileStream);
                var file = new ProductPhoto { ProductId = model.ProductId, Path = path,  };
                _uow.ProductPhotos.Add(file);
            }
            await _uow.SaveChangesAsync();
            return Ok();
        }
    }
}