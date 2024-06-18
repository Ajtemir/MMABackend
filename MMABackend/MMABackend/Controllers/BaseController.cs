using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MMABackend.DataAccessLayer;

namespace MMABackend.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly ILogger<BaseController> _logger;

        public BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;
        }

        protected ActionResult Execute(Action action)
        {
            try
            {
                action.Invoke();
                return Ok(Result.Ok());
            }
            catch (ApplicationException e)
            {
                _logger.Log(LogLevel.Warning, e.Message);
                return BadRequest(Result.Bad(e.Message, e.StackTrace));
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
                return BadRequest(Result.Bad(e.Message, e.StackTrace, true));
            }
        }

        protected ActionResult Execute<T>(Func<T> func) where T:class
        {
            try
            {
                return Ok(Result.Ok(
                    typeof(T) == typeof(Task)
                        ? null
                        : func.Invoke()));
            }
            catch (ApplicationException e)
            {
                _logger.Log(LogLevel.Warning, e.Message);
                return BadRequest(Result.Bad(e.Message, e.StackTrace));
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
                return BadRequest(Result.Bad(e.Message, e.StackTrace, true));
            }
        }

        protected string UserId => User.Identity?.Name ?? throw new ApplicationException("Не найден идентификатор пользователя");
    }

    public class Result<T> : Result
    {
        public T Data { get; set; } = default;
    }

    public class Result
    {
        public bool IsOk { get; set; } = false;
        public string Message { get; set; } = null;
        public string StackTrace { get; set; } = null;
        public bool IsError { get; set; } = false;
        public static Result Ok() => new()
        {
            IsOk = true,
        };
        
        public static Result<TOk> Ok<TOk>(TOk data, string message = null) => new()
        {
            IsOk = true,
            Data = data,
            Message = message,
        };
        
        public static Result Bad(string message, string stackTrace, bool isError = false) => new()
        {
            IsOk = false,
            Message = message,
            IsError = isError,
            StackTrace = stackTrace,
        };
        
        public static Result<TBad> Bad<TBad>(string message, string stackTrace, bool isError = false, TBad data = default) => new()
        {
            IsOk = false,
            Message = message,
            Data = data,
            IsError = isError,
            StackTrace = stackTrace,
        };
    }
}