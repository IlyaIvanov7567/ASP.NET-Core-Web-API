using System;
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
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IRepository<HddMetric> _repository;
        private readonly IMapper _mapper;

        public HddMetricsController(ILogger<HddMetricsController> logger, IRepository<HddMetric> repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        
        [HttpPost("create")]
        public IActionResult Create([FromBody] MetricCreateRequest<HddMetric> request)
        {
            _repository.Create(new HddMetric
            {
                Time = request.Time,
                Value = request.Value
            });

            _logger.LogInformation("Time: {0}; Value: {1}", request.Time, request.Value);
            return Ok();
        }
        
        [HttpGet("getbyinterval/from/{fromTime}/to/{toTime}")]
        public IActionResult GetByInterval([FromRoute] long fromTime, [FromRoute] long toTime)
        {
            var metrics = _repository.GetByInterval(fromTime, toTime);

            var response = new AllMetricsResponse<HddMetric>()
            {
                Metrics = new List<MetricDto<HddMetric>>()
            };
            
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<MetricDto<HddMetric>>(metric));
            }

            return Ok(response);
        }
        
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _repository.GetAll();

            var response = new AllMetricsResponse<HddMetric>()
            {
                Metrics = new List<MetricDto<HddMetric>>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<MetricDto<HddMetric>>(metric));
            }

            return Ok(response);
        }
    }
}
