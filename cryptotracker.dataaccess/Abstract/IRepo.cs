using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cryptotracker.dataaccess.Abstract
{
    public interface IRepo<T>
    {
        List<T> GetAll();
        T GetById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}