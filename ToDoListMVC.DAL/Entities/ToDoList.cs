using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListMVC.DAL.Entities
{
    public class ToDoList : BaseEntity
    {
        public ToDoList()
        {
            Shares = new List<ToDoListShare>();
        }
        public string Title { get; set; }
        public virtual List<ToDoListShare> Shares { get; set; }
    }
}
