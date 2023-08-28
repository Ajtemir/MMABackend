using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MMABackend.DataAccessLayer;

namespace MMABackend.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public partial class CollectiveTradeController : BaseController
    {
        private readonly UnitOfWork _uow;
        public CollectiveTradeController(ILogger<CollectiveTradeController> logger, UnitOfWork uow) : base(logger)
        {
            _uow = uow;
        }
    }
}