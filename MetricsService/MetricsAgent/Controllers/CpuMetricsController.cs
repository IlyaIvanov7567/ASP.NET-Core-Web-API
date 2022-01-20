using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MetricsAgent.DAL;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using MetricsAgent.Models;
using System.Collections.Generic;
using AutoMapper;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly IRepository<CpuMetric> _repository;
        private readonly IMapper _mapper;

        public CpuMetricsController(ILogger<CpuMetricsController> logger, IRepository<CpuMetric> repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
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
                response.Metrics.Add(_mapper.Map<MetricDto<CpuMetric>>(metric));
            }

            return Ok(response);
        }
    }
}
