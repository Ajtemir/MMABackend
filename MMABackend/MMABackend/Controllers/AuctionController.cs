using Microsoft.Extensions.Logging;
using MMABackend.DataAccessLayer;

namespace MMABackend.Controllers
{
    public partial class AuctionController : ContextBaseController
    {
        public AuctionController(ILogger<AuctionController> logger, UnitOfWork uow) : base(logger, uow)
        {
        }
    }
}