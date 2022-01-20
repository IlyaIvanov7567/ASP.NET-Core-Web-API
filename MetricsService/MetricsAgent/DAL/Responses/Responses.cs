using System;
using System.Collections.Generic;

namespace MetricsAgent.Responses
{
    public class AllMetricsResponse<T>
    {
        public List<MetricDto<T>> Metrics { get; set; }
    }

    public class MetricDto<T>
    {
        public TimeSpan Time { get; set; }
       
        public int Value { get; set; }
        
        public int Id { get; set; }
    }
}
