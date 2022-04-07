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
    public class ToDoItemService : IToDoItemService
    {
        private readonly IRepository<ToDoItem> _toDoItemRepository;
        public ToDoItemService(IRepository<ToDoItem> toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository;
        }

        public bool AssignToDoItemToUser(int toDoItemId, string userId)
        {
            var item = _toDoItemRepository.GetById(toDoItemId);

            if (item != null)
            {
                item.Assigns.Add(new ToDoItemAssign()
                {
                    ToDoItemId = toDoItemId,
                    UserId = userId
                });
            }
            return _toDoItemRepository.Update(item);
        }

        public bool Create(int toDoListId, string title, string description, bool isComplete, string userId)
        {
            return _toDoItemRepository.Create(new ToDoItem()
            {
                ToDoListId = toDoListId,
                Title = title,
                Description = description,
                IsComplete = isComplete,
                CreatedBy = userId,
                CreatedAt = DateTime.Now
            });

        }

        public List<ToDoItem> GetAll()
        {
            return _toDoItemRepository.GetAll();
        }

        public ToDoItem GetToDoItemById(int toDoItemId)
        {
            return _toDoItemRepository.GetById(toDoItemId);
        }

        public List<ToDoItem> GetToDoItemsByToDoList(int toDoListId)
        {
            return _toDoItemRepository.Find(item => item.ToDoListId == toDoListId);
        }

        public List<ToDoItem> GetToDoItemsByUser(string userId)
        {
            return _toDoItemRepository.Find(item => item.Assigns.Any(assign => assign.UserId == userId && assign.ToDoItemId == item.Id));
        }

        public bool Remove(int toDoItemId)
        {
            return _toDoItemRepository.RemoveById(toDoItemId);
        }

        public bool ToggleComplete(int toDoItemId)
        {
            var item = _toDoItemRepository.GetById(toDoItemId);
            item.IsComplete = !item.IsComplete;
            return _toDoItemRepository.Update(item);
        }

        public bool Update(int toDoItemId, int toDoListId, string title, string description, bool isComplete, string userId)
        {
            var item = _toDoItemRepository.GetById(toDoItemId);
            item.ToDoListId = toDoListId;
            item.Title = title;
            item.Description = description;
            item.IsComplete = isComplete;
            item.ModifiedAt = DateTime.Now;
            item.ModifiedBy = userId;
            return _toDoItemRepository.Update(item);
        }
    }
}
