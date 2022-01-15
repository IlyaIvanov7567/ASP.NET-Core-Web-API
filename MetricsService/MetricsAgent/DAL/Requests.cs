using System;

namespace MetricsAgent.Requests
{
    public class MetricCreateRequest<T>
    {
        public TimeSpan Time { get; set; }
        public int Value { get; set; }
    }
}