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

        protected ActionResult Execute<T>(Func<T> func)
        {
            try
            {
                return Ok(func.Invoke());
            }
            catch (ApplicationException e)
            {
                _logger.Log(LogLevel.Warning, e.Message);
                return BadRequest(Result.Bad(e.Message));
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
                return BadRequest(Result.Bad(e.Message));
            }
        }
    }

    public class Result<T> : Result
    {
   
        public T Data { get; set; } = default;
        public static Result<T> Ok(T data) => new()
        {
            IsOk = true,
            Data = data,
        };

        public static Result<T> Bad(string message, T data) => new()
        {
            IsOk = false,
            Message = message,
            Data = data,
        };
    }

    public class Result
    {
        public bool IsOk { get; set; } = false;
        public string Message { get; set; } = null;
        public static Result Ok() => new()
        {
            IsOk = true,
        };
        
        public static Result Bad(string message) => new()
        {
            IsOk = false,
            Message = message,
        };
    }
}