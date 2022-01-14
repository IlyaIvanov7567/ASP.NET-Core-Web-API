using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;

        public DotNetMetricsController(ILogger<DotNetMetricsController> logger)
        {
            _logger = logger;
        }
        
        [HttpPost("api/metrics/dotnet/errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult PostMetricsFromAgent([FromBody] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("AgentId: {0}; fromTime: {1}; toTime: {2}", agentId, fromTime, toTime);
            return Ok();
        }
    }
}
