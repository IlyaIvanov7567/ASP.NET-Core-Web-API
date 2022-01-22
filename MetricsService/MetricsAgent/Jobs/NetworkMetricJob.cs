using MetricsAgent.DAL;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.Models;

namespace MetricsAgent.Jobs
{
    public class NetworkMetricJob : IJob
    {
        private readonly IRepository<NetworkMetric> _repository;

        private readonly PerformanceCounter _perfomanceCounter;

        public NetworkMetricJob(IRepository<NetworkMetric> repository)
        {
            _repository = repository;

            PerformanceCounterCategory category = new PerformanceCounterCategory("Network Interface");

            String[] instancename = category.GetInstanceNames();

            foreach (var item in instancename)
            {
                _perfomanceCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", item);
            }
        }

        public Task Execute(IJobExecutionContext context)
        {
            var bytesPerSecReceived = Convert.ToInt32(_perfomanceCounter.NextValue());

            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new Models.NetworkMetric {Time = time, Value = bytesPerSecReceived});

            return Task.CompletedTask;
        }
    }
}