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
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IMetricsAgentClient _metricsAgentClientagent;
        
        public HddMetricsController (ILogger<HddMetricsController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet("/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            var response = _metricsAgentClientagent.GetHddMetrics(new MetricGetRequest<HddMetric>(fromTime, toTime));

            if (response == null)
                return Problem();
            
            return Ok(response);
        }
    }
}