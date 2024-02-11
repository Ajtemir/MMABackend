using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MMABackend.DataAccessLayer;
using MMABackend.DomainModels.Common;

namespace MMABackend.Controllers
{
    [ApiController]
    [Route("")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly UnitOfWork _uow;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, UnitOfWork uow)
        {
            _logger = logger;
            _uow = uow;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok("It is working");
        }
        
        [HttpGet("[action]")]
        public ActionResult Delete()
        {
            _uow.Database.EnsureDeleted();
            return Ok();
        }
        
        [HttpGet("[action]")]
        public ActionResult Test()
        {
            return Ok();
        }

        private class Counter
        {
            public Counter(string ip, int count)
            {
                Ip = ip;
                Count = count;
            }
            public string Ip { get; set; }
            public int Count { get; set; }
        }
    }
}