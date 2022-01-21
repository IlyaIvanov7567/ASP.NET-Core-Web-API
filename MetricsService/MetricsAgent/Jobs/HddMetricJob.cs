using MetricsAgent.DAL;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.Models;

namespace MetricsAgent.Jobs
{
    public class HddMetricJob : IJob
    {
        private readonly IRepository<HddMetric> _repository;
        
        private readonly PerformanceCounter _perfomanceCounter;
        
        public HddMetricJob(IRepository<HddMetric> repository)
        {
            _repository = repository;
            _perfomanceCounter = new PerformanceCounter("PhysicalDisk", "Disk Read Bytes/sec", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var hddReadInBytes = Convert.ToInt32(_perfomanceCounter.NextValue());
            
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new Models.HddMetric { Time = time, Value = hddReadInBytes });
            
            return Task.CompletedTask;
        }
    }
}