﻿using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Core.DAL.Models;
using Core.Interfaces;

namespace MetricsAgent.Jobs
{
    public class NetworkMetricJob : IJob
    {
        private readonly IRepository<NetworkMetric> _repository;

        public NetworkMetricJob(IRepository<NetworkMetric> repository)
        {
            _repository = repository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            PerformanceCounterCategory category = new PerformanceCounterCategory("Network Interface");

            String[] instancename = category.GetInstanceNames();

            int bytesPerSecReceivedTotal = 0;

            PerformanceCounter _performanceСounter;

            foreach (var item in instancename)
            {
                _performanceСounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", item);

                bytesPerSecReceivedTotal += Convert.ToInt32(_performanceСounter.NextValue());
            }

            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new NetworkMetric {Time = time, Value = bytesPerSecReceivedTotal});

            return Task.CompletedTask;
        }
    }
}