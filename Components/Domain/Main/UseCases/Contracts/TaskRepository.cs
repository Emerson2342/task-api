using Microsoft.EntityFrameworkCore;
using TaskList.Components.Domain.Infra.Data;
using TaskList.Components.Domain.Main.Entities;

namespace TaskList.Components.Domain.Main.UseCases.Contracts
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AnyTaskByTitleAsync(string title)
        { 
        return await _context.Tasks.AnyAsync(task => task.Title == title);
        }

        public async Task<TaskEntity> GetTaskById(Guid id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(task => task.UserId == id);
        }

        public async Task<TaskEntity> GetTaskByTitle(string title)
        {
            return await _context.Tasks.FirstOrDefaultAsync(task => task.Title == title);
        }

        public async Task SaveAsync(TaskEntity task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }
    }
}
