using MetricsAgent.DAL;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.Models;

namespace MetricsAgent.Jobs
{
    public class CpuMetricJob : IJob
    {
        private readonly IRepository<CpuMetric> _repository;
        
        private readonly PerformanceCounter _cpuCounter;
        
        public CpuMetricJob(IRepository<CpuMetric> repository)
        {
            _repository = repository;
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var cpuUsageInPercents = Convert.ToInt32(_cpuCounter.NextValue());
            
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new Models.CpuMetric { Time = time, Value = cpuUsageInPercents });
            
            return Task.CompletedTask;
        }
    }
}