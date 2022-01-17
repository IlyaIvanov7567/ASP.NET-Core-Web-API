using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MetricsAgent.DAL;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using MetricsAgent.Models;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly IRepository<NetworkMetric> _repository;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, IRepository<NetworkMetric> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] MetricCreateRequest<NetworkMetric> request)
        {
            _repository.Create(new NetworkMetric
            {
                Time = request.Time,
                Value = request.Value
            });

            _logger.LogInformation("Time: {0}; Value: {1}", request.Time, request.Value);
            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _repository.GetAll();

            var response = new AllMetricsResponse<NetworkMetric>()
            {
                Metrics = new List<MetricDto<NetworkMetric>>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new MetricDto<NetworkMetric> { Time = metric.Time, Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }
    }
}
