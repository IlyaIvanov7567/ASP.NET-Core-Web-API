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
    [Route("api/metricsmanager/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly IMetricsAgentClient _metricsAgentClient;
        private readonly IRepository<NetworkMetric> _repository;
        private readonly IMapper _mapper;
        
        public NetworkMetricsController (ILogger<NetworkMetricsController> logger, IMapper mapper, IRepository<NetworkMetric> repository, IMetricsAgentClient metricsAgentClient)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
            _metricsAgentClient = metricsAgentClient;
        }
        
        [HttpGet("fromagent/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] long fromTime, [FromRoute] long toTime)
        {
            var response = _metricsAgentClient.GetNetworkMetrics(new MetricGetRequest<NetworkMetric>(fromTime, toTime));

            if (response == null)
                return Problem();
            
            return Ok(response);
        }
        
        [HttpGet("getbyinterval/from/{fromTime}/to/{toTime}")]
        public IActionResult GetByInterval([FromRoute] long fromTime, [FromRoute] long toTime)
        {
            var metrics = _repository.GetByInterval(fromTime, toTime);

            var response = new MetricsResponse<NetworkMetric>()
            {
                Metrics = new List<MetricDto<NetworkMetric>>()
            };
            
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<MetricDto<NetworkMetric>>(metric));
            }

            return Ok(response);
        }
        
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var metrics = _repository.GetAll();

            var response = new MetricsResponse<NetworkMetric>()
            {
                Metrics = new List<MetricDto<NetworkMetric>>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<MetricDto<NetworkMetric>>(metric));
            }

            return Ok(response);
        }
    }
}
