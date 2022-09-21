using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonalTaskManager.Models;
using PersonalTaskManager.Models.ViewModels;

namespace PersonalTaskManager.Controllers
{
    public class MyTasksController : Controller
    {
        private TaskManagerDbContext _context;
        public int PageSize = 4;

        public MyTasksController(TaskManagerDbContext context)
        {
            _context = context;
        }

        // GET: MyTasks
        public IActionResult Index(string? category, int taskPage = 1)
        {
            return View(new MyTasksViewModel
            {
                MyTasks = _context.MyTasks
                .Where(t => category == null || t.TaskCategory==category)
                .OrderBy(c => c.Id)
              .Skip((taskPage - 1) * PageSize)
              .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = taskPage,
                    ItemsPerPage = PageSize,
                    TotalItems = category==null ? _context.MyTasks.Count() : _context.MyTasks.Where(t => t.TaskCategory == category).Count()
                }, CurrentCategory = category
            });
        }

        // GET: MyTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MyTasks == null)
            {
                return NotFound();
            }

            var myTask = await _context.MyTasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myTask == null)
            {
                return NotFound();
            }

            return View(myTask);
        }

        // GET: MyTasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MyTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,TaskStatus,TaskCategory")] MyTask myTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(myTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(myTask);
        }

        // GET: MyTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MyTasks == null)
            {
                return NotFound();
            }

            var myTask = await _context.MyTasks.FindAsync(id);
            if (myTask == null)
            {
                return NotFound();
            }
            return View(myTask);
        }

        // POST: MyTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,TaskStatus,TaskCategory")] MyTask myTask)
        {
            if (id != myTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(myTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MyTaskExists(myTask.Id))
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
            return View(myTask);
        }

        // GET: MyTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MyTasks == null)
            {
                return NotFound();
            }

            var myTask = await _context.MyTasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myTask == null)
            {
                return NotFound();
            }

            return View(myTask);
        }

        // POST: MyTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MyTasks == null)
            {
                return Problem("Entity set 'TaskManagerDbContext.MyTasks'  is null.");
            }
            var myTask = await _context.MyTasks.FindAsync(id);
            if (myTask != null)
            {
                _context.MyTasks.Remove(myTask);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MyTaskExists(int id)
        {
          return _context.MyTasks.Any(e => e.Id == id);
        }
    }
}
