using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class ProductImageController
    {
        [HttpGet("{productImageId:int}")]
        public ActionResult ProductImage(int productImageId)
        {
            var image = _uow.ProductPhotos.FirstOrError(x=> x.Id == productImageId);
            return new FileContentResult(image.File, contentType: image.ContentType);
        }
    }
}