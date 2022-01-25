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
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        private readonly IRepository<DotNetMetric> _repository;
        private readonly IMapper _mapper;

        public DotNetMetricsController(ILogger<DotNetMetricsController> logger, IRepository<DotNetMetric> repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        
        [HttpPost("create")]
        public IActionResult Create([FromBody] MetricCreateRequest<DotNetMetric> request)
        {
            _repository.Create(new DotNetMetric
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

            var response = new MetricsResponse<DotNetMetric>()
            {
                Metrics = new List<MetricDto<DotNetMetric>>()
            };
            
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<MetricDto<DotNetMetric>>(metric));
            }

            return Ok(response);
        }
        
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _repository.GetAll();

            var response = new MetricsResponse<DotNetMetric>()
            {
                Metrics = new List<MetricDto<DotNetMetric>>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<MetricDto<DotNetMetric>>(metric));
            }

            return Ok(response);
        }
    }
}
