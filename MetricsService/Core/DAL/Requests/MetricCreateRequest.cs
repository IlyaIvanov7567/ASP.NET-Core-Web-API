using System;

namespace Core.DAL.Requests
{
    public class MetricCreateRequest<T>
    {
        public TimeSpan Time { get; set; }
        
        public int Value { get; set; }
    }
}