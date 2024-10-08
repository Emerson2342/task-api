using TaskList.Components.Domain.Main.ValueObjects;
using TaskList.Components.Domain.Shared.Entities;

namespace TaskList.Components.Domain.Main.Entities
{
    public class TaskEntity : Entity
    {
        public string UserId { get; set; }
        public List<ValueObjects.Task> Tasks { get; set; } = new();

        protected TaskEntity() { }

        public TaskEntity(List<ValueObjects.Task> task)
        {
            Tasks = task;
        }
    }
}
