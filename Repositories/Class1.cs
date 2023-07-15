using System;
using System.Collections.Generic;

namespace Repositories
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(int i);
        void Create(T objectCreate);
        void Update(T objectCreate);
        void Delete(int id);
    }
}
