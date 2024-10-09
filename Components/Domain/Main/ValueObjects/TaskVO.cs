using TaskList.Components.Domain.Shared.ValueObjects;

namespace TaskList.Components.Domain.Main.ValueObjects
{
    public class TaskVO : ValueObject
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime Deadline { get; set; }

        protected TaskVO() { }

        public TaskVO(string title, string description, DateTime deadline)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description))
                throw new Exception("Favor preencher todos os campos!");

            if(deadline <= DateTime.UtcNow)
                throw new Exception("Prazo final incorreto!");
            {
                Title = title;
                Description = description;
                Deadline = deadline;
            }
        }
    }
}
