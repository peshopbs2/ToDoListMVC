using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListMVC.BLL.Abstractions;
using ToDoListMVC.BLL.Services;
using ToDoListMVC.DAL.Entities;
using ToDoListMVC.Models.Identity;
using ToDoListMVC.Models.ViewModels.ToDoItems;

namespace ToDoListMVC.Controllers
{
    public class ToDoItemsController : Controller
    {
        private IToDoItemService _toDoItemService;
        private UserManager<AppUser> _userManager;
        public ToDoItemsController(IToDoItemService toDoItemService, UserManager<AppUser> userManager)
        {
            _toDoItemService = toDoItemService;
            _userManager = userManager;
        }
        // GET: ToDoItemsController
        public async Task<ActionResult> Index()
        {
            List<ToDoItem> toDoItems = null;
            if (User.IsInRole("Admin"))
            {
                toDoItems = _toDoItemService
                    .GetAll();
            }
            else
            {
                var user = await _userManager.GetUserAsync(User);
                toDoItems = _toDoItemService.GetToDoItemsByUser(user.Id);
            }
            var items = toDoItems
                .Select(item => new ToDoItemViewModel()
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    IsComplete = item.IsComplete,
                    Assignees = string.Join(", ", item.Assigns
                    .Select(assign => _userManager.GetUserNameAsync(
                        _userManager.FindByIdAsync(assign.UserId).Result
                        ).Result
                    )
                    .ToList()),
                    ToDoListTitle = item.ToDoList.Title,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedBy,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedBy
                }) 
                .ToList();

            return View(items);
        }

        // GET: ToDoItemsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ToDoItemsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDoItemsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ToDoItemsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ToDoItemsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ToDoItemsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ToDoItemsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
