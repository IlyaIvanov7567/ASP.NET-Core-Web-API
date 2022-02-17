using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Core.DAL.Models;
using Core.Interfaces;

namespace MetricsAgent.Jobs
{
    public class DotNetMetricJob : IJob
    {
        private readonly IRepository<DotNetMetric> _repository;
        
        private readonly PerformanceCounter _performanceСounter;
        
        public DotNetMetricJob(IRepository<DotNetMetric> repository)
        {
            _repository = repository;
            
            _performanceСounter = new PerformanceCounter(".NET CLR Memory", "# bytes in all heaps", Process.GetCurrentProcess().ProcessName);
        }

        public Task Execute(IJobExecutionContext context)
        {
            var heapUsageInBytes = Convert.ToInt32(_performanceСounter.NextValue());
            
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new DotNetMetric { Time = time, Value = heapUsageInBytes });
            
            return Task.CompletedTask;
        }
    }
}