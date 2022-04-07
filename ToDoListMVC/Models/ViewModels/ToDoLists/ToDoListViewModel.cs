﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListMVC.Models.ViewModels.ToDoLists
{
    public class ToDoListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
