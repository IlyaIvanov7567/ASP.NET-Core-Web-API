using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Core.DAL.Models;
using Core.DAL.Requests;
using MetricsManager.Clients;


namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IMetricsAgentClient metricsAgentClient;
        
        public HddMetricsController (ILogger<HddMetricsController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] long fromTime, [FromRoute] long toTime)
        {
            var response = metricsAgentClient.GetHddMetrics(new MetricGetRequest<HddMetric>(fromTime, toTime));

            if (response == null)
                return Problem();
            
            return Ok(response);
        }
    }
}