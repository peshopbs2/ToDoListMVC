using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListMVC.DAL.Entities;

namespace ToDoListMVC.BLL.Abstractions
{
    public interface IToDoItemService
    {
        bool Create(int toDoListId, string title, string description, bool isComplete, string userId);
        bool Update(int toDoItemId, int toDoListId, string title, string description, bool isComplete, string userId);
        List<ToDoItem> GetAll();
        ToDoItem GetToDoItemById(int toDoItemId);
        bool Remove(int toDoItemId);
        List<ToDoItem> GetToDoItemsByUser(string userId);
        List<ToDoItem> GetToDoItemsByToDoList(int toDoListId);
        bool ToggleComplete(int toDoItemId);

        bool AssignToDoItemToUser(int toDoItemId, string userId);

        List<string> GetUsersWithAccessToToDoItem(int toDoItemId);
    }
}
