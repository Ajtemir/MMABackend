using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MMABackend.DataAccessLayer;
using MMABackend.DomainModels.Common;

namespace MMABackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            // int milliseconds = 10000;
            // Thread.Sleep(milliseconds);
            // var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            // _uow.IpRequestCounters.Add(new IpRequestCounter { Ip = ip });
            // _uow.SaveChanges();
            // var dict = _uow.IpRequestCounters
            //     .GroupBy(x => x.Ip)
            //     .Select(x => new Counter(x.Key,x.Count()));
            // return Ok(dict);
            // var rng = new Random();
            // return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //     {
            //         Date = DateTime.Now.AddDays(index),
            //         TemperatureC = rng.Next(-20, 55),
            //         Summary = Summaries[rng.Next(Summaries.Length)],
            //         Ip = HttpContext.Connection.RemoteIpAddress?.ToString()
            //     })
            //     .ToArray();
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