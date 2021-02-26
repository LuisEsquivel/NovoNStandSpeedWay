

using Api.Interface;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository
{

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private ApplicationDbContext _context;
        private DbSet<T> table = null;


        public GenericRepository(ApplicationDbContext context)
        {
            this._context = context;
            table = _context.Set<T>();
        }


        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }


        public T GetById(object id)
        {
            return table.Find(id);
        }

        public IEnumerable<T> GetByValues(Func<T, bool> values)
        {
            return table.Where(values).AsEnumerable();
        }

        public bool Exist(Func<T, bool> values)
        {
            return table.Where(values).AsEnumerable().Count() > 0 ? true : false;
        }

        public bool Add(T obj)
        {
            table.Add(obj);
            return Save();
        }

        public bool Update(T obj, object id = null)
        {
            if (id != null)
            {
                var exist = table.Find(id);
                if (exist != null) { _context.Entry(exist).State = EntityState.Detached; }
            };

            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            return Save();
        }

        public bool Delete(object id)
        {
            T row = table.Find(id);

            if (row != null)
            {
                table.Remove(row);
                return Save();
            }

            return false;
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }

    }
}
