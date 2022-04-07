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
        private readonly IRepository<ToDoListShare> _shareRepository;
        public ToDoListService(IRepository<ToDoList> toDoListRepostiory, IRepository<ToDoListShare> shareRepository)
        {
            _toDoListRepository = toDoListRepostiory;
            _shareRepository = shareRepository;
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
           
            var lists = _toDoListRepository
                .Find(item => item.CreatedBy == userId);

            var sharedLists = _shareRepository.Find(item => item.UserId == userId)
                .Select(item => item.ToDoList)
                .ToList();

            lists.AddRange(sharedLists);

            return lists
                .OrderBy(item => item.Id)
                .ToList();
        }

        public bool Remove(int toDoListId)
        {
            return _toDoListRepository.Remove(
                _toDoListRepository.GetById(toDoListId)
            );
        }

        public bool RemoveShare(int toDoListId, string userId)
        {
            var share = _shareRepository.Get(item => item.ToDoListId == toDoListId && item.UserId == userId);
            if (share != null)
            {
                return _shareRepository.Remove(share);
            }
            else
            {
                return false;
            }
        }

        public bool Share(int toDoListId, string userId, string sharedById)
        {
            var toDoList = GetToDoListById(toDoListId);
            if (toDoList == null)
            {
                //no todo list, nothing to do here...
                return false;
            }

            bool alreadyShared = _shareRepository.Get(item => item.ToDoListId == toDoListId && item.UserId == userId) != null;
            if(alreadyShared)
            {
                //already shared
                return false;
            }

            bool result = _shareRepository.Create(new ToDoListShare()
            {
                ToDoListId = toDoListId,
                UserId = userId,
                CreatedBy = sharedById
            });

            return result;
        }

        public bool Update(int toDoListId, string title, string userId)
        {
            var toDoList = _toDoListRepository.GetById(toDoListId);
            toDoList.Title = title;
            toDoList.ModifiedAt = DateTime.Now;
            toDoList.ModifiedBy = userId;
            return _toDoListRepository.Update(toDoList);
        }
    }
}
