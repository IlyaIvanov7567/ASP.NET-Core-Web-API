using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Core.DAL.Models;
using Core.Interfaces;

namespace MetricsAgent.Jobs
{
    public class RamMetricJob : IJob
    {
        private readonly IRepository<RamMetric> _repository;
        
        private readonly PerformanceCounter _performanceСounter;
        
        public RamMetricJob(IRepository<RamMetric> repository)
        {
            _repository = repository;
            _performanceСounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var ramAvailable = Convert.ToInt32(_performanceСounter.NextValue());
            
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new RamMetric { Time = time, Value = ramAvailable });
            
            return Task.CompletedTask;
        }
    }
}