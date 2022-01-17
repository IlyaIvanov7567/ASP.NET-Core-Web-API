using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MetricsAgent.DAL;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using MetricsAgent.Models;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly IRepository<CpuMetric> _repository;

        public CpuMetricsController (ILogger<CpuMetricsController> logger, IRepository<CpuMetric> repository)
        {
            _logger = logger;
            _repository = repository;
        }
        
        [HttpPost("create")]
        public IActionResult Create([FromBody] MetricCreateRequest<CpuMetric> request)
        {
            _repository.Create(new CpuMetric
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

            var response = new AllMetricsResponse<CpuMetric>()
            {
                Metrics = new List<MetricDto<CpuMetric>>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new MetricDto<CpuMetric> { Time = metric.Time, Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }
    }
}
