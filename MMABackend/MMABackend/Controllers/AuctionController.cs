using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MMABackend.DataAccessLayer;

namespace MMABackend.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public partial class AuctionController : ContextBaseController
    {
        public AuctionController(ILogger<AuctionController> logger, UnitOfWork uow) : base(logger, uow)
        {
        }
    }
}