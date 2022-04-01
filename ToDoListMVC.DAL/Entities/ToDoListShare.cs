using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListMVC.DAL.Entities
{
    public class ToDoListShare : BaseEntity
    {
        public int ToDoListId { get; set; }
        public virtual ToDoList ToDoList { get; set; }
        public string UserId { get; set; }
    }
}
