namespace PersonalTaskManager.Models
{
    public class MyTask
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string TaskStatus { get; set; } = string.Empty;
        public string TaskCategory { get; set; } = string.Empty;    
    }
}
