using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Core.DAL.Models;
using Core.Interfaces;

namespace MetricsAgent.Jobs
{
    public class HddMetricJob : IJob
    {
        private readonly IRepository<HddMetric> _repository;
        
        private readonly PerformanceCounter _performanceСounter;
        
        public HddMetricJob(IRepository<HddMetric> repository)
        {
            _repository = repository;
            _performanceСounter = new PerformanceCounter("PhysicalDisk", "Disk Read Bytes/sec", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var diskReadInBytesPerSec = Convert.ToInt32(_performanceСounter.NextValue());
            
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new HddMetric { Time = time, Value = diskReadInBytesPerSec });
            
            return Task.CompletedTask;
        }
    }
}