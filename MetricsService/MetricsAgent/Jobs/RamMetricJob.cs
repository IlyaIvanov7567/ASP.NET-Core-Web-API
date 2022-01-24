using MetricsAgent.DAL;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.Models;

namespace MetricsAgent.Jobs
{
    public class RamMetricJob : IJob
    {
        private readonly IRepository<RamMetric> _repository;
        
        private readonly PerformanceCounter _perfomanceCounter;
        
        public RamMetricJob(IRepository<RamMetric> repository)
        {
            _repository = repository;
            _perfomanceCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var RamAvailable = Convert.ToInt32(_perfomanceCounter.NextValue());
            
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new Models.RamMetric { Time = time, Value = RamAvailable });
            
            return Task.CompletedTask;
        }
    }
}