using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListMVC.Models.ViewModels.ToDoItems
{
    public class AssignToDoItemViewModel
    {
        public int ToDoItemId { get; set; }
        public string Title { get; set; }
        public List<SelectListItem> Users { get; set; }
        public string AssignUserId { get; set; }
    }
}
