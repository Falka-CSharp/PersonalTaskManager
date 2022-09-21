using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using PersonalTaskManager.Models;
namespace PersonalTaskManager.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private TaskManagerDbContext repository;
        public NavigationMenuViewComponent(TaskManagerDbContext repo)
        {
            repository = repo;
        }
        public IViewComponentResult Invoke()
        {
            return View(repository.MyTasks
                .Select(t => t.TaskCategory)
                .Distinct()
                .OrderBy(t => t));
        }
    }
}