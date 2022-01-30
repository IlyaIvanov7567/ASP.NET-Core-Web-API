using System;
using System.Data.SQLite;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Models;
using Core.DAL.Requests;
using Core.Interfaces;
using Dapper;
using MetricsManager.Clients;
using MetricsManager.DAL.Repositories;
using Quartz;

namespace MetricsManager.Jobs
{
    public class CpuMetricJob : IJob
    {
        private readonly IRepository<CpuMetric> _repository;
        private readonly IMetricsAgentClient _agentClient;
        private readonly IMapper _mapper;

        private const string ConnectionString =
            @"Data Source=metricsmanager.db; Version=3;Pooling=True;Max Pool Size=100;";

        public CpuMetricJob(
            IRepository<CpuMetric> repository, 
            IMetricsAgentClient metricsAgentClient, 
            IMapper mapper)
        {
            _repository = repository;
            _agentClient = metricsAgentClient;
            _mapper = mapper;
        }

        public Task Execute(IJobExecutionContext context)
        {
            long lastsynctime = GetLastSyncTime();
            
            long GetLastSyncTime()
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    long lastsynctime = connection.QuerySingle<long>("SELECT time FROM cpumetrics order by time desc limit 1");
                    return lastsynctime;
                }
            }

            var response = _agentClient.GetCpuMetrics(new MetricGetRequest<CpuMetric>(lastsynctime, DateTime.Now.Ticks));

            foreach (var metricDto in response.Metrics)
            {
                var cpuMetric = _mapper.Map<CpuMetric>(metricDto);
                _repository.Create(cpuMetric);
            }
            
            return Task.CompletedTask;
        }
    }
}