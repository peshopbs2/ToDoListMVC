using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListMVC.DAL.Entities
{
    public class ToDoItemAssign : BaseEntity
    {
        public int ToDoItemId{ get; set; }
        public virtual ToDoItem ToDoItem { get; set; }
        public string UserId { get; set; }
    }
}
