using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Core.DAL.Models;
using Core.Interfaces;
using Dapper;

namespace MetricsAgent.DAL.Repositories
{
    public class NetworkMetricsRepository : IRepository<NetworkMetric>
    {
        private const string ConnectionString = @"Data Source=metrics.db; Version=3;Pooling=True;Max Pool Size=100;";

        public NetworkMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }

        public void Create(NetworkMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("INSERT INTO networkmetrics(value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time.TotalSeconds
                    });
            }
        }

        public NetworkMetric GetById(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<NetworkMetric>("SELECT Id, Time, Value FROM networkmetrics WHERE id=@id",
                    new {id = id});
            }
        }
        
        public IList<NetworkMetric> GetByInterval(long fromTime, long toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection
                    .Query<NetworkMetric>("SELECT Id, Time, Value FROM networkmetrics WHERE time>@fromTime AND time<@toTime;",
                        new
                        {
                            fromTime = fromTime, 
                            toTime = toTime
                        })
                    .ToList();
            }
        }

        public IList<NetworkMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<NetworkMetric>("SELECT Id, Time, Value FROM networkmetrics").ToList();
            }
        }

        public void Update(NetworkMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("UPDATE networkmetrics SET value = @value, time = @time WHERE id=@id",
                    new
                    {
                        value = item.Value,
                        time = item.Time.TotalSeconds,
                        id = item.Id
                    });
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("DELETE FROM networkmetrics WHERE id=@id",
                    new
                    {
                        id = id
                    });
            }
        }
    }
}