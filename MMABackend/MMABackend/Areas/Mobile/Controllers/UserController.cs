using Microsoft.AspNetCore.Mvc;

namespace MMABackend.Areas.Mobile.Controllers
{
    [Area("Mobile")]
    [ApiController]
    [Route("[area]/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetId(int i)
        {
            return Ok(i);
        }
    }
}