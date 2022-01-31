using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MetricsAgent.DAL;
using System.Collections.Generic;
using AutoMapper;
using Core.DAL.Models;
using Core.DAL.Requests;
using Core.DAL.Responses;
using Core.Interfaces;

namespace MetricsAgent.Controllers
{
    [Route("api/metricsagent/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IRepository<RamMetric> _repository;
        private readonly IMapper _mapper;

        public RamMetricsController(ILogger<RamMetricsController> logger, IRepository<RamMetric> repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        
        [HttpPost("create")]
        public IActionResult Create([FromBody] MetricCreateRequest<RamMetric> request)
        {
            _repository.Create(new RamMetric
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

            var response = new MetricsResponse<RamMetric>()
            {
                Metrics = new List<MetricDto<RamMetric>>()
            };
            
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<MetricDto<RamMetric>>(metric));
            }

            return Ok(response);
        }
        
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _repository.GetAll();

            var response = new MetricsResponse<RamMetric>()
            {
                Metrics = new List<MetricDto<RamMetric>>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<MetricDto<RamMetric>>(metric));
            }

            return Ok(response);
        }
    }
}
