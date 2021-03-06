using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListMVC.DAL.Entities;

namespace ToDoListMVC.BLL.Abstractions
{
    public interface IToDoListService
    {
        bool Create(string title, string userId);
        bool Update(int toDoListId, string title, string userId);
        List<ToDoList> GetAll();
        ToDoList GetToDoListById(int toDoListId);
        bool Remove(int toDoListId, string userId);
        List<ToDoList> GetToDoListsByUser(string userId);
        bool Share(int toDoListId, string userId, string sharedBy);
        bool RemoveShare(int toDoListId, string userId);
        bool IsShared(int toDoListId, string userId);
    }
}
