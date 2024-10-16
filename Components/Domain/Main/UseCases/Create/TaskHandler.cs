using System.Collections.Generic;
using System.Threading.Tasks;
using TaskList.Components.Domain.Main.DTOs.TaskDTOs;
using TaskList.Components.Domain.Main.Entities;
using TaskList.Components.Domain.Main.UseCases.Contracts;
using TaskList.Components.Domain.Main.UseCases.ResponseCase;
using TaskList.Components.Domain.Main.ValueObjects;
using static TaskList.Components.Domain.Main.Entities.TaskEntity;

namespace TaskList.Components.Domain.Main.UseCases.Create
{
    public class TaskHandler
    {
        private readonly ITaskRepository _repository;

        public TaskHandler(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response> CreateTask(RequestTask newTask)
        {
            var exists = await _repository.AnyTaskByTitleAsync(newTask.Title);

            if (exists)
                return new Response("Já existe uma tarefa com esse nome", 401);

            var taskResult = TaskEntity.With(newTask.UserId, newTask.Title, newTask.Description, newTask.StartTime.Value, newTask.Deadline.Value);

            //in case null object from taskResult
            if (taskResult.TaskEntity == null)
                return taskResult.Response;

            await _repository.SaveAsync(taskResult.TaskEntity);
            return taskResult.Response;
        }

        public async Task<Response> EditTask(RequestTask taskToEdit)
        {
            var exists = await _repository.AnyTaskByIdAsync(taskToEdit.Id);

            // return new Response($"Tarefa editada com sucesso! {exists}", 201);

            if (!exists) return new Response($"Tarefa para editar não encontrada! Id: {taskToEdit.Id}", 400);

            var originalTask = await _repository.GetTaskById(taskToEdit.Id);
            // return new Response($"Tarefa editada com sucesso! {originalTask.ToString}", 201);

            var task = TaskEntity.Edit(originalTask, taskToEdit);

            await _repository.UpdateAsync(task);

            return new Response("Tarefa editada com sucesso!", 201);
        }
        public async Task<Response> DeleteTask(Guid id)
        {
            var exists = await _repository.AnyTaskByIdAsync(id);
            if (!exists) return new Response("Tarefa não encontrada!", 400);

            var task = await _repository.GetTaskById(id);

            await _repository.DeleteTaskAsync(task);

            return new Response("Tarefa removida com sucesso!", 201);
        }

        public async Task<Response> ListTask()
        {
            try
            {
                 List<TaskEntity> tasks= await _repository.ListTasks();
                return new Response("Atividade listadas com sucess!", tasks);
            }
            catch  {
                return new Response("Erro ao listar as atividades!", 500);
            }
            
        }

        public async Task<Response> GetTask(Guid id)
        {
            try
            {
                var task = await _repository.GetTaskById(id);
                return new Response("Tarefa encontrada!", task);
            }
            catch
            {
                return new Response("Erro ao buscar tarefa!", 500);
            }

        }
    }
}
