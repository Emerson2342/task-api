using TaskList.Components.Domain.Main.Entities;

namespace TaskList.Components.Domain.Main.UseCases.Contracts
{
    public interface ITaskRepository
    {
        Task<bool> AnyTaskByTitleAsync(string title);
        Task<bool> AnyTaskByIdAsync(Guid id);
        Task<TaskEntity> GetTaskById(Guid id);
        Task<TaskEntity> GetTaskByTitle(string title);
        Task SaveAsync(TaskEntity task);
        Task UpdateAsync(TaskEntity task);
        Task DeleteTaskAsync(TaskEntity task);
        Task<List<TaskEntity>> ListTasks();


    }
}
