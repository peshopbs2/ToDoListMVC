using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListMVC.DAL.Abstractions
{
    public interface IRepository<T>
    {
        T Get(Func<T, bool> predicate);
        T GetById(int id);
        List<T> GetAll();
        List<T> Find(Func<T, bool> predicate);
        bool Create(T entity);
        bool Update(T entity);
        bool Remove(T entity);
        bool RemoveById(int id);
    }
}
