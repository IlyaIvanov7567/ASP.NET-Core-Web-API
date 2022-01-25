using System;
using System.Data.Common;

namespace MetricsAgent.Requests
{
    public class MetricGetRequest<T>
    {
        public MetricGetRequest(TimeSpan fromTime, TimeSpan toTime)
        {
            FromTime = fromTime;
            ToTime = ToTime;
        }
        
        public TimeSpan FromTime { get; private set; }
       
        public TimeSpan ToTime { get; private set; }
    }
}