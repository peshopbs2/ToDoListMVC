using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoListMVC.Models.Identity;
using ToDoListMVC.Models.ViewModels.ToDoItems;

namespace ToDoListMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ToDoListMVC.Models.ViewModels.ToDoItems.ToDoItemViewModel> ToDoItemViewModel { get; set; }
        public DbSet<ToDoListMVC.Models.ViewModels.ToDoItems.CreateToDoItemViewModel> CreateToDoItemViewModel { get; set; }
    }
}
