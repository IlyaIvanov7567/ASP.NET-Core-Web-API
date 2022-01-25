using System;
using System.Collections.Generic;

namespace Core.DAL.Responses
{
    public class MetricsResponse<T>
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
