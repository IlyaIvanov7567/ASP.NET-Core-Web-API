using System;

namespace Core.DAL.Models
{
    public class RamMetric
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public TimeSpan Time { get; set; }
    }
}
