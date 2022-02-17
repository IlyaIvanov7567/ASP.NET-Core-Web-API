using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using AutoMapper;
using Core.DAL.Models;
using Core.DAL.Requests;
using Core.DAL.Responses;
using Core.Interfaces;
using MetricsManager.Clients;


namespace MetricsManager.Controllers
{
    [Route("api/metricsmanager/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IMetricsAgentClient _metricsAgentClient;
        private readonly IRepository<RamMetric> _repository;
        private readonly IMapper _mapper;
        
        public RamMetricsController (ILogger<RamMetricsController> logger, IMapper mapper, IRepository<RamMetric> repository, IMetricsAgentClient metricsAgentClient)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
            _metricsAgentClient = metricsAgentClient;
        }
        
        [HttpGet("fromagent/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] long fromTime, [FromRoute] long toTime)
        {
            var response = _metricsAgentClient.GetRamMetrics(new MetricGetRequest<RamMetric>(fromTime, toTime));

            if (response == null)
                return Problem();
            
            return Ok(response);
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
        
        [HttpGet("getall")]
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
