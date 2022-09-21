using Microsoft.EntityFrameworkCore;
namespace PersonalTaskManager.Models
{
    public class TaskManagerDbContext : DbContext 
    {
        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options)
            : base(options) { }
        public DbSet<MyTask> MyTasks => Set<MyTask>();
    }
}
