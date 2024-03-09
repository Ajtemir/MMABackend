using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MMABackend.DataAccessLayer;

namespace MMABackend.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public partial class GroupDiscountController : BaseController
    {
        private readonly UnitOfWork _uow;
        public GroupDiscountController(ILogger<GroupDiscountController> logger, UnitOfWork uow) : base(logger)
        {
            _uow = uow;
        }
    }
}