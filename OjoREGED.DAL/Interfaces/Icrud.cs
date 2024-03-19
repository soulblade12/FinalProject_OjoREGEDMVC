using System.Collections.Generic;

namespace OjoREGED.DAL.Interfaces
{
    public interface Icrud<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
