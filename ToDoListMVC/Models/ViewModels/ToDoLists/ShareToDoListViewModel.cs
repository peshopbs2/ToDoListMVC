using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListMVC.Models.ViewModels.Users;

namespace ToDoListMVC.Models.ViewModels.ToDoLists
{
    public class ShareToDoListViewModel
    {
        public int ToDoListId { get; set; }
        public string Title { get; set; }
        public List<SelectListItem> Users { get; set; }
        public string ShareUserId { get; set; }
    }
}
