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
            var toDoList = _toDoListService.GetToDoListById(id);

            return View(new ToDoListViewModel()
            {
                Id = toDoList.Id,
                Title = toDoList.Title,
                CreatedAt = toDoList.CreatedAt,
                CreatedBy = toDoList.CreatedBy, 
                ModifiedAt = toDoList.ModifiedAt,
                ModifiedBy = toDoList.ModifiedBy
            });
        }

        // GET: ToDoListsController/Create
        public ActionResult Create()
        {
            return View(new CreateToDoListViewModel());
        }

        // POST: ToDoListsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] CreateToDoListViewModel model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                bool result = _toDoListService.Create(model.Title, user.Id);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: ToDoListsController/Edit/5
        public ActionResult Edit(int id)
        {
            var toDoList = _toDoListService.GetToDoListById(id);

            return View(new CreateToDoListViewModel()
            {
                Title = toDoList.Title
            });
        }

        // POST: ToDoListsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [FromForm] CreateToDoListViewModel model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);

                _toDoListService.Update(id, model.Title, user.Id);
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
            var toDoList = _toDoListService.GetToDoListById(id);

            return View(new ToDoListViewModel()
            {
                Id = toDoList.Id,
                Title = toDoList.Title,
                CreatedAt = toDoList.CreatedAt,
                CreatedBy = toDoList.CreatedBy,
                ModifiedAt = toDoList.ModifiedAt,
                ModifiedBy = toDoList.ModifiedBy
            });
        }

        // POST: ToDoListsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _toDoListService.Remove(id);
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
