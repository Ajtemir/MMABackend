using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MMABackend.DataAccessLayer;

namespace MMABackend.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public partial class MarketController : BaseController
    {
        private readonly UnitOfWork _uow;
        public MarketController(ILogger<MarketController> logger, UnitOfWork uow) : base(logger)
        {
            _uow = uow;
        }
    }
}