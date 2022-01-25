using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Core.DAL.Models;
using Core.DAL.Requests;
using MetricsManager.Clients;


namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IMetricsAgentClient metricsAgentClient;
        
        public RamMetricsController (ILogger<RamMetricsController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            var response = metricsAgentClient.GetRamMetrics(new MetricGetRequest<RamMetric>(fromTime, toTime));

            if (response == null)
                return Problem();
            
            return Ok(response);
        }
    }
}