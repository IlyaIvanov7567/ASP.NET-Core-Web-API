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
    [Route("api/metricsmanager/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly IMetricsAgentClient _metricsAgentClient;
        private readonly IRepository<CpuMetric> _repository;
        private readonly IMapper _mapper;
        
        public CpuMetricsController (
            ILogger<CpuMetricsController> logger, 
            IMapper mapper, 
            IRepository<CpuMetric> repository, 
            IMetricsAgentClient metricsAgentClient)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
            _metricsAgentClient = metricsAgentClient;
        }
        
        [HttpGet("fromagent/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] long fromTime, [FromRoute] long toTime)
        {
            var response = _metricsAgentClient.GetCpuMetrics(new MetricGetRequest<CpuMetric>(fromTime, toTime));

            if (response == null)
                return Problem();
            
            return Ok(response);
        }
        
        [HttpGet("getbyinterval/from/{fromTime}/to/{toTime}")]
        public IActionResult GetByInterval([FromRoute] long fromTime, [FromRoute] long toTime)
        {
            var metrics = _repository.GetByInterval(fromTime, toTime);

            var response = new MetricsResponse<CpuMetric>()
            {
                Metrics = new List<MetricDto<CpuMetric>>()
            };
            
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<MetricDto<CpuMetric>>(metric));
            }

            return Ok(response);
        }
        
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var metrics = _repository.GetAll();

            var response = new MetricsResponse<CpuMetric>()
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
