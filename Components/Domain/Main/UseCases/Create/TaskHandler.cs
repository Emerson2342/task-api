using TaskList.Components.Domain.Main.DTOs.TaskDTOs;
using TaskList.Components.Domain.Main.Entities;
using TaskList.Components.Domain.Main.UseCases.Contracts;
using TaskList.Components.Domain.Main.UseCases.ResponseCase;

namespace TaskList.Components.Domain.Main.UseCases.Create
{
    public class TaskHandler
    {
        private readonly ITaskRepository _repository;

        public TaskHandler(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response> CreateTask(RequestCreateTask newTask)
        {
            var exists = await _repository.AnyTaskByTitleAsync(newTask.Title);

            if (exists)
                return new Response("Já existe uma tarefa com esse nome", 401);

           // if(string.IsNullOrEmpty(new) || string.IsNullOrEmpty(description))

            try
            {
                TaskEntity task = new(newTask.UserId, newTask.Title, newTask.Description, newTask.StartTime, newTask.Deadline);
                await _repository.SaveAsync(task);
                return new Response("Tarefa criada com sucesso!", new ResponseDataTask(task.Title, task.StartTime, task.Deadline));
            }
            catch (Exception ex)
            {
                return new Response($"Não foi possível criar uma nova tarefa. {ex.Message}", 500);
            }

        }

        public async Task<Response> EditTask(RequestCreateTask newTask)
        {
            return null;
        }
        public async Task<Response> DeleteTask(RequestCreateTask newTask)
        {
            return null;
        }
    }
}
