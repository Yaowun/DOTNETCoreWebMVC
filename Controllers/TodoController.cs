using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DOTNETCoreWebMVC.Data;
using DOTNETCoreWebMVC.Models;
using DOTNETCoreWebMVC.Models.Enum;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DOTNETCoreWebMVC.Controllers
{
    public class TodoController(TodoContext todoContext) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await todoContext.TodoModel.ToListAsync());
        }
        
        public IActionResult Create()
        {
            ViewData["StatusList"] = new SelectList(Enum.GetValues(typeof(TodoStatus)).Cast<TodoStatus>());
            ViewData["TypeList"] = new SelectList(Enum.GetValues(typeof(TodoType)).Cast<TodoStatus>());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Status,Type")] TodoModel todoModel)
        {
            if (ModelState.IsValid)
            {
                todoModel.Title = todoModel.Title.Trim();
                todoModel.CreatedAt = DateTime.Now;
                todoModel.ModifiedAt = DateTime.Now;
                todoContext.Add(todoModel);
                await todoContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["StatusList"] = new SelectList(Enum.GetValues(typeof(TodoStatus)).Cast<TodoStatus>());
            ViewData["TypeList"] = new SelectList(Enum.GetValues(typeof(TodoType)).Cast<TodoStatus>());
            return View(todoModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoModel = await todoContext.TodoModel.FindAsync(id);
            if (todoModel == null)
            {
                return NotFound();
            }
            
            ViewData["StatusList"] = new SelectList(Enum.GetValues(typeof(TodoStatus)).Cast<TodoStatus>());
            ViewData["TypeList"] = new SelectList(Enum.GetValues(typeof(TodoType)).Cast<TodoStatus>());
            return View(todoModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Status,Type,CreatedAt")] TodoModel todoModel)
        {
            if (id != todoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    todoModel.Title = todoModel.Title.Trim();
                    todoModel.ModifiedAt = DateTime.Now;
                    todoContext.Update(todoModel);
                    await todoContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoModelExists(todoModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(todoModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoModel = await todoContext.TodoModel.FirstOrDefaultAsync(m => m.Id == id);
            if (todoModel == null)
            {
                return NotFound();
            }

            return View(todoModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todoModel = await todoContext.TodoModel.FindAsync(id);
            if (todoModel != null)
            {
                todoContext.TodoModel.Remove(todoModel);
            }

            await todoContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoModelExists(int id)
        {
            return todoContext.TodoModel.Any(e => e.Id == id);
        }
    }
}
