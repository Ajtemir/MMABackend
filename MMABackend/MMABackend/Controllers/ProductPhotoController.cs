using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace MMABackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductPhotoController : ControllerBase
    {
        private readonly IWebHostEnvironment  _environment;

        public ProductPhotoController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return Ok(_environment.WebRootFileProvider);

        }
    }
}