using MetricsAgent.DAL;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.Models;

namespace MetricsAgent.Jobs
{
    public class CpuMetricJob : IJob
    {
        private IRepository<CpuMetric> _repository;

        public CpuMetricJob(IRepository<CpuMetric> repository)
        {
            _repository = repository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("ping");
            
            return Task.CompletedTask;
        }
    }
}