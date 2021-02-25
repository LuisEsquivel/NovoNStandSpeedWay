using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        IEnumerable<T> GetByValues(Func<T, bool> values);
        bool Exist(Func<T, bool> values);
        bool Add(T obj);
        bool Update(T obj, int id = 0);
        bool Delete(object id);
        bool Save();

    }
}
