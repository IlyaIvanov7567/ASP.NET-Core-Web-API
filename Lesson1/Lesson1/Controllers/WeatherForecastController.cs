using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Lesson1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ValuesHolder _holder;
        
        public WeatherForecastController(ValuesHolder holder)
        {
            _holder = holder;
        }
        
        [HttpGet ("get_current")]
        public WeatherForecast Get([FromQuery] DateTime date)
        {
            var rng = new Random();
            return new WeatherForecast
            {
                Date = date,
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            };
        }
        
        [HttpGet("get_by_date")]
        public List<WeatherForecast> Get([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            return _holder.Read(fromDate, toDate);
        }

        [HttpPost("create")]
        public void Post([FromQuery] DateTime date)
        {
            var rng = new Random();
            _holder.Add(new WeatherForecast
            {
                Date = date,
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }
        
        [HttpPatch("update")]
        public void Patch([FromQuery] DateTime date, [FromQuery] int newValue)
        {
            _holder.Patch(date, newValue);
        }

        [HttpDelete("delete")]
        public void Delete([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            _holder.Delete(fromDate, toDate);
        }
    }
}