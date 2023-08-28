using Microsoft.Extensions.Logging;
using MMABackend.DataAccessLayer;

namespace MMABackend.Controllers
{
    public partial class CollectiveTradeController : BaseController
    {
        private readonly UnitOfWork _uow;
        public CollectiveTradeController(ILogger<CollectiveTradeController> logger, UnitOfWork uow) : base(logger)
        {
            _uow = uow;
        }
    }
}