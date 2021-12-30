using System;
using System.Collections.Generic;

namespace Lesson1
{
    public class ValuesHolder
    {
        public List<WeatherForecast> Values = new List<WeatherForecast>();

        public void Add(WeatherForecast weather)
        {
            Values.Add(weather);
        }

        public void Patch(DateTime date, int newValue)
        {
            foreach (var item in Values)
            {
                if (item.Date == date)
                {
                    item.TemperatureC = newValue;
                }
            }
        }

        public void Delete(DateTime fromDate, DateTime toDate)
        {
            foreach (var item in Values)
            {
                if (item.Date >= fromDate && item.Date <= toDate)
                {
                    Values.Remove(item);
                }
            }
        }

        public List<WeatherForecast> Read(DateTime fromDate, DateTime toDate)
        {
            List<WeatherForecast> result = new List<WeatherForecast>();
            foreach (var item in Values)
            {
                if (item.Date >= fromDate && item.Date <= toDate)
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }
}