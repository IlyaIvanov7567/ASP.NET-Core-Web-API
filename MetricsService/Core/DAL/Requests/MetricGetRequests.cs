using System;

namespace Core.DAL.Requests
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