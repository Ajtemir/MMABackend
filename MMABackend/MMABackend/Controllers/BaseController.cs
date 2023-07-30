using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MMABackend.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly ILogger<BaseController> _logger;

        public BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;
        }
        protected ActionResult Execute(Func<ActionResult> func)
        {
            try
            {
                return func.Invoke();
            }
            catch (ApplicationException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
    
}