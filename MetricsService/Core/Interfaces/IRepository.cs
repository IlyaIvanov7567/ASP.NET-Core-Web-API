using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Create(T item);

        T GetById(int id);

        IList<T> GetByInterval(long fromTime, long toTime);

        IList<T> GetAll();
        
        void Update(T item);

        void Delete(int id);

        IList<T> GetFromDate(long fromTime);
    }
}