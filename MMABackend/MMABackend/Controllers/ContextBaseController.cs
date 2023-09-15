using Microsoft.Extensions.Logging;
using MMABackend.DataAccessLayer;

namespace MMABackend.Controllers
{
    public class ContextBaseController : BaseController
    {
        protected readonly UnitOfWork Uow;
        public ContextBaseController(ILogger<ContextBaseController> logger, UnitOfWork uow) : base(logger)
        {
            Uow = uow;
        }
    }
}