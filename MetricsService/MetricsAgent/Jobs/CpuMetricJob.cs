using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Core.DAL.Models;
using Core.Interfaces;

namespace MetricsAgent.Jobs
{
    public class CpuMetricJob : IJob
    {
        private readonly IRepository<CpuMetric> _repository;
        
        private readonly PerformanceCounter _performanceСounter;
        
        public CpuMetricJob(IRepository<CpuMetric> repository)
        {
            _repository = repository;
            _performanceСounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var cpuUsageInPercents = Convert.ToInt32(_performanceСounter.NextValue());
            
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new CpuMetric { Time = time, Value = cpuUsageInPercents });
            
            return Task.CompletedTask;
        }
    }
}