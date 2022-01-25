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
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly IMetricsAgentClient _metricsAgentClient;
        
        public CpuMetricsController (ILogger<CpuMetricsController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet("/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            var response = _metricsAgentClient.GetCpuMetrics(new MetricGetRequest<CpuMetric>(fromTime, toTime));

            if (response == null)
                return Problem();
            
            return Ok(response);
        }
    }
}
