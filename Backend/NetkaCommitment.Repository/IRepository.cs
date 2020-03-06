using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetkaCommitment.Repository
{
    public interface IRepository<T>
    {
        public IQueryable<T> Get();
        public T Get(T obj);
        public bool Insert(T obj);
        public bool Update(T obj);
        public bool Delete(T obj);
    }
}
