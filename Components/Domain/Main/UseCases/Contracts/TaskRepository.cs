using Microsoft.EntityFrameworkCore;
using TaskList.Components.Domain.Infra.Data;
using TaskList.Components.Domain.Main.DTOs.TaskDTOs;
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

        public async Task<bool> AnyTaskByIdAsync(Guid id)
        {
            return await _context.Tasks.AnyAsync(task => task.Id == id);
        }

        public async Task<TaskEntity> GetTaskById(Guid id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(task => task.Id == id);
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

        public async Task UpdateAsync(TaskEntity task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(TaskEntity task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TaskEntity>> ListTasks()
        {
            return await _context.Tasks.ToListAsync();
        }


    }
}
