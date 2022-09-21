namespace PersonalTaskManager.Models.ViewModels
{
    public class MyTasksViewModel
    {
        public IEnumerable<MyTask> MyTasks { get; set; } = Enumerable.Empty<MyTask>();
        public PagingInfo PagingInfo { get; set; } = new();
        public string? CurrentCategory { get; set; }
    }
}
