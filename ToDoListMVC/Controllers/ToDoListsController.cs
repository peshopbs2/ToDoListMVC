using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListMVC.BLL.Abstractions;
using ToDoListMVC.DAL.Entities;
using ToDoListMVC.Models.Identity;
using ToDoListMVC.Models.ViewModels.ToDoLists;

namespace ToDoListMVC.Controllers
{
    [Authorize]
    public class ToDoListsController : Controller
    {
        private IToDoListService _toDoListService;
        private UserManager<AppUser> _userManager;
        public ToDoListsController(IToDoListService toDoListService, UserManager<AppUser> userManager)
        {
            _toDoListService = toDoListService;
            _userManager = userManager;
        }
        // GET: ToDoListsController
        public async Task<ActionResult> Index()
        {
            List<ToDoList> toDoListsData = null;
            if (User.IsInRole("Admin"))
            {
                toDoListsData = _toDoListService
                    .GetAll();
            }
            else
            {
                var user = await _userManager.GetUserAsync(User);
                toDoListsData = _toDoListService.GetToDoListsByUser(user.Id);
            }
            var toDoLists = toDoListsData
                .Select(item => new ToDoListViewModel()
                {
                    Id = item.Id,
                    Title = item.Title,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedBy,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedBy
                })
                .ToList();

            return View(toDoLists);
        }

        // GET: ToDoListsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ToDoListsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDoListsController/Create
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

        // GET: ToDoListsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ToDoListsController/Edit/5
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

        // GET: ToDoListsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ToDoListsController/Delete/5
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
