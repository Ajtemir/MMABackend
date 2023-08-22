using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MMABackend.DataAccessLayer;

namespace MMABackend.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public partial class ShopsController : BaseController
    {
        private readonly UnitOfWork _uow;
        public ShopsController(ILogger<ShopsController> logger, UnitOfWork uow) : base(logger)
        {
            _uow = uow;
        }
    }
}