using TaskList.Components.Domain.Main.ValueObjects;
using TaskList.Components.Domain.Shared.Entities;

namespace TaskList.Components.Domain.Main.Entities
{
    public class TaskEntity : Entity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime Deadline { get; set; }

        protected TaskEntity() { }

        public TaskEntity(User user, string title, string description, DateTime startTime, DateTime deadline)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description))
                throw new Exception("Favor preencher todos os campos!");

            if (deadline <= DateTime.UtcNow)
                throw new Exception("Prazo final incorreto!");

            Title = title;
            Description = description;
            Deadline = deadline;
            User = user;
            Title = title;
            Description = description;
            StartTime = startTime;
            Deadline = deadline;
            UserId = user.Id;
        }
    }
}
