using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListMVC.BLL.Abstractions;
using ToDoListMVC.DAL.Abstractions;
using ToDoListMVC.DAL.Entities;

namespace ToDoListMVC.BLL.Services
{
    public class ToDoListService : IToDoListService
    {
        private readonly IRepository<ToDoList> _toDoListRepository;
        public ToDoListService(IRepository<ToDoList> toDoListRepostiory)
        {
            _toDoListRepository = toDoListRepostiory;
        }

        public bool Create(string title, string userId)
        {
            return _toDoListRepository.Create(new ToDoList()
            {
                Title = title,
                CreatedBy = userId,
                CreatedAt = DateTime.Now
            });
        }

        public List<ToDoList> GetAll()
        {
            return _toDoListRepository.GetAll();
        }

        public ToDoList GetToDoListById(int toDoListId)
        {
            return _toDoListRepository.GetById(toDoListId);
        }

        public List<ToDoList> GetToDoListsByUser(string userId)
        {
            //TODO: change the predicate so we see shared and not only created by us lists
            return _toDoListRepository
                .Find(item => item.CreatedBy == userId);
        }

        public bool Remove(int toDoListId)
        {
            return _toDoListRepository.Remove(
                _toDoListRepository.GetById(toDoListId)
            );
        }

        public bool Update(int toDoListId, string title)
        {
            var toDoList = _toDoListRepository.GetById(toDoListId);
            toDoList.Title = title;
            return _toDoListRepository.Update(toDoList);
        }
    }
}
