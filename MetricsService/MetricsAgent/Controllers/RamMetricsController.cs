using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;

        public RamMetricsController(ILogger<RamMetricsController> logger)
        {
            _logger = logger;
        }
        [HttpPost("api/metrics/ram/available/from/{fromTime}/to/{toTime}")]
        public IActionResult PostMetricsFromAgent([FromBody] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("fromTime: {0}; toTime: {1}", fromTime, toTime);
            return Ok();
        }
    }
}
