using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private IToDoListService _toDoListService;
        private UserManager<AppUser> _userManager;
        public ToDoItemsController(IToDoItemService toDoItemService, IToDoListService toDoListService, UserManager<AppUser> userManager)
        {
            _toDoItemService = toDoItemService;
            _toDoListService = toDoListService;
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
            var item = _toDoItemService.GetToDoItemById(id);
            if(item==null)
            {
                return NotFound();
            }
            var model = new ToDoItemViewModel()
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
                ToDoListId = item.ToDoListId,
                CreatedAt = item.CreatedAt,
                CreatedBy = item.CreatedBy,
                ModifiedAt = item.ModifiedAt,
                ModifiedBy = item.ModifiedBy
            };
            return View(model);
        }

        // GET: ToDoItemsController/Create
        public ActionResult Create(int id)
        {
            var toDoList = _toDoListService.GetToDoListById(id);
            if(toDoList==null)
            {
                return NotFound();
            }

            return View();
        }

        // POST: ToDoItemsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int id, [FromForm] CreateToDoItemViewModel model)
        {
            try
            {
                var toDoList = _toDoListService.GetToDoListById(id);
                if (toDoList == null)
                {
                    return NotFound();
                }
                var user = await _userManager.GetUserAsync(User);

                _toDoItemService.Create(id, model.Title, model.Description, model.IsComplete, user.Id);
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
            var item = _toDoItemService.GetToDoItemById(id);
            if (item == null)
            {
                return NotFound();
            }
            var model = new CreateToDoItemViewModel()
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                IsComplete = item.IsComplete,
                ToDoListId = item.ToDoListId
            };
            return View(model);
        }

        // POST: ToDoItemsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [FromForm] CreateToDoItemViewModel model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);

                _toDoItemService.Update(id, model.ToDoListId, model.Title, model.Description, model.IsComplete, user.Id);
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
            var item = _toDoItemService.GetToDoItemById(id);
            if (item == null)
            {
                return NotFound();
            }
            var model = new ToDoItemViewModel()
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
                ToDoListId = item.ToDoListId,
                CreatedAt = item.CreatedAt,
                CreatedBy = item.CreatedBy,
                ModifiedAt = item.ModifiedAt,
                ModifiedBy = item.ModifiedBy
            };
            return View(model);
        }

        // POST: ToDoItemsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _toDoItemService.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Assign(int id)
        {
            var toDoItem = _toDoItemService.GetToDoItemById(id);
            var user = await _userManager.GetUserAsync(User);

            var users = _toDoItemService.GetUsersWithAccessToToDoItem(toDoItem.Id)
                .Select(userId => new SelectListItem()
                {
                    Text = _userManager.FindByIdAsync(userId).Result.UserName,
                    Value = userId
                })
                .ToList();
            var assignees = toDoItem.Assigns.Select(item => item.UserId);
            foreach (var userId in assignees)
            {
                users.RemoveAll(item => item.Value == userId);
            }
            return View(new AssignToDoItemViewModel()
            {
                Title = toDoItem.Title,
                Users = users
            });
        }

        // POST: ToDoListsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Assign(int id, [FromForm] AssignToDoItemViewModel model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var result = _toDoItemService.AssignToDoItemToUser(id, model.AssignUserId);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("AssignUserId", "The todo item is already assigned with that user.");
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Toggle(int id)
        {
            var item = _toDoItemService.GetToDoItemById(id);
            if(item==null)
            {
                return NotFound();
            }

            _toDoItemService.ToggleComplete(id);
            return RedirectToAction("Index");
        }
    }
}
