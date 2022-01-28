﻿using System.Data.SQLite;
using System.Threading.Tasks;
using Core.DAL.Models;
using Core.Interfaces;
using Dapper;
using MetricsManager.Clients;
using Quartz;

namespace MetricsManager.Jobs
{
    public class HddMetricJob : IJob
    {
        private IRepository<HddMetric> _repository;
        private readonly IMetricsAgentClient _metricsAgentClient;
        private const string ConnectionString = @"Data Source=metricsmanager.db; Version=3;Pooling=True;Max Pool Size=100;";
        
        public HddMetricJob(IRepository<HddMetric> repository, IMetricsAgentClient metricsAgentClient)
        {
            _repository = repository;
            _metricsAgentClient = metricsAgentClient;
        }

        public Task Execute(IJobExecutionContext context)
        {
            long GetLastSyncTime()
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    long lastSyncTime = connection.QuerySingle<long>("SELECT MAX (Time) FROM hddmetrics");
                    return lastSyncTime;
                }
            }
            return Task.CompletedTask;
        }
    }
}