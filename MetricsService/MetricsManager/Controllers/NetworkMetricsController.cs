using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Core.DAL.Models;
using Core.DAL.Requests;
using MetricsManager.Clients;


namespace MetricsManager.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly IMetricsAgentClient metricsAgentClient;
        
        public NetworkMetricsController (ILogger<NetworkMetricsController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            var response = metricsAgentClient.GetNetworkMetrics(new MetricGetRequest<NetworkMetric>(fromTime, toTime));

            if (response == null)
                return Problem();
            
            return Ok(response);
        }
    }
}