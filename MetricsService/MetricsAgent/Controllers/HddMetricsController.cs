using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;

        public HddMetricsController(ILogger<HddMetricsController> logger)
        {
            _logger = logger;
        }

        [HttpPost("api/metrics/hdd/left/from/{fromTime}/to/{toTime}")]
        public IActionResult PostMetricsFromAgent([FromBody] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("AgentId: {0}; fromTime: {1}; toTime: {2}", agentId, fromTime, toTime);
            return Ok();
        }
    }
}
