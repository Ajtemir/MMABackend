using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MMABackend.DataAccessLayer;

namespace MMABackend.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public partial class ReductionController : ContextBaseController
    {
        public ReductionController(ILogger<ReductionController> logger, UnitOfWork uow) : base(logger, uow)
        {
        }
    }
}