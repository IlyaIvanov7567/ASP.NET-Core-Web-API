using MetricsAgent.DAL;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.Models;

namespace MetricsAgent.Jobs
{
    public class DotNetMetricJob : IJob
    {
        private readonly IRepository<DotNetMetric> _repository;
        
        private readonly PerformanceCounter _perfomanceCounter;
        
        public DotNetMetricJob(IRepository<DotNetMetric> repository)
        {
            _repository = repository;
            
            _perfomanceCounter = new PerformanceCounter(".NET CLR Memory", "# bytes in all heaps", Process.GetCurrentProcess().ProcessName);
        }

        public Task Execute(IJobExecutionContext context)
        {
            var heapUsageInBytes = Convert.ToInt32(_perfomanceCounter.NextValue());
            
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new Models.DotNetMetric { Time = time, Value = heapUsageInBytes });
            
            return Task.CompletedTask;
        }
    }
}