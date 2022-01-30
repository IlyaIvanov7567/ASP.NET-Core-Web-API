using System;

namespace Core.DAL.Requests
{
    public class MetricGetRequest<T>
    {
        public MetricGetRequest(long fromTime, long toTime)
        {
            FromTime = fromTime;
            ToTime = toTime;
        }
        public long FromTime { get; private set; }
       
        public long ToTime { get; private set; }
    }
}