using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListMVC.DAL.Abstractions;
using ToDoListMVC.DAL.Data;

namespace ToDoListMVC.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ToDoDbContext _context;
        public Repository(ToDoDbContext dbContext)
        {
            _context = dbContext;
        }
        public bool Create(T entity)
        {
            entity.CreatedAt = DateTime.Now;
            _context.Set<T>().Add(entity);
            return _context.SaveChanges() != 0;
        }

        public List<T> Find(Func<T, bool> predicate)
        {
            //.Set<T> tells us which DbSet<T> we use for the given T entity
            //predicate = lambda expression, ex.: item => item.Value > 0
            return _context
                .Set<T>()
                .Where(predicate)
                .ToList();
        }

        public T Get(Func<T, bool> predicate)
        {
            return _context.
                Set<T>()
                .FirstOrDefault(predicate);
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public bool Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
            return _context.SaveChanges() != 0;
        }

        public bool RemoveById(int id)
        {
            var item = _context.Set<T>().FirstOrDefault(item => item.Id == id);
            if (item != null)
            {
                _context.Set<T>()
                    .Remove(item);
                return _context.SaveChanges() != 0;

            }
            else
            {
                //I'm deleting non-existent ID
                return false;
            }
        }

        public bool Update(T entity)
        {
            if (entity.Id != 0)
            {
                entity.ModifiedAt = DateTime.Now;
                _context.Update(entity);
                return _context.SaveChanges() != 0;
            }
            else
            {
                //Idk what to change because no ID
                return false;
            }
        }
    }
}
