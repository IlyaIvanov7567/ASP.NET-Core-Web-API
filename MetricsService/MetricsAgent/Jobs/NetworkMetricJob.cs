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

        public NetworkMetricJob(IRepository<NetworkMetric> repository)
        {
            _repository = repository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            PerformanceCounterCategory category = new PerformanceCounterCategory("Network Interface");

            String[] instancename = category.GetInstanceNames();

            int bytesPerSecReceivedTotal = 0;

            PerformanceCounter perfomanceCounter;

            foreach (var item in instancename)
            {
                perfomanceCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", item);

                bytesPerSecReceivedTotal += Convert.ToInt32(perfomanceCounter.NextValue());
            }

            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new Models.NetworkMetric {Time = time, Value = bytesPerSecReceivedTotal});

            return Task.CompletedTask;
        }
    }
}