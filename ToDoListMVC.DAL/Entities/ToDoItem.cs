using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListMVC.DAL.Entities
{
    public class ToDoItem : BaseEntity
    {
        public ToDoItem()
        {
            Assigns = new List<ToDoItemAssign>();
        }
        public int ToDoListId { get; set; }
        public virtual ToDoList ToDoList { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public virtual List<ToDoItemAssign> Assigns { get; set; }
    }
}
