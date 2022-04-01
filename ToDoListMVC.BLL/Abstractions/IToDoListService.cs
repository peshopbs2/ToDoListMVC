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
        bool Create(string title);
        bool Update(int toDoListId, string title);
        List<ToDoList> GetAll();
        ToDoList GetToDoListById(int toDoListId);
        bool Remove(int toDoListId);
        List<ToDoList> GetToDoListsByUser(string userId);
    }
}
