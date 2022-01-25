using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using MetricsManager.Client;


namespace MetricsManager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        private readonly IMetricsAgentClient metricsAgentClient;
        
        public DotNetMetricsController (ILogger<DotNetMetricsController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet("/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            var response = metricsAgentClient.GetDotNetMetrics(new MetricGetRequest<DotNetMetric>(fromTime, toTime));

            if (response == null)
                return Problem();
            
            return Ok(response);
        }
    }
}