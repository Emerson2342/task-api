﻿using System.Collections.Generic;
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

            var taskResult = New(newTask.UserId, newTask.Title, newTask.Description, newTask.StartTime, newTask.Deadline, newTask.PhotoTask);

        
            if (taskResult.TaskEntity == null)
                return taskResult.Response;

            await _repository.SaveAsync(taskResult.TaskEntity);
            return taskResult.Response;
        }

        public async Task<Response> EditTask(RequestTask taskToEdit)
        {
            var originalTask = await _repository.GetTaskById(taskToEdit.Id);

            if (originalTask == null)
            {
                return new Response($"Tarefa para editar não encontrada! Id: {taskToEdit.Id}", 400);
            }

            var taskEdited = Edit(originalTask, taskToEdit);

            if (taskEdited.TaskEntity == null)
                return taskEdited.Response;


            await _repository.UpdateAsync(taskEdited.TaskEntity);

            return new Response($"Tarefa editada com sucesso! {taskEdited.TaskEntity.Title}", 201);
        }
        public async Task<Response> DeleteTask(Guid id)
        {
            var task = await _repository.GetTaskById(id);
            if (task == null)
                return new Response($"Tarefa não encontrada! Id: {id}", 400);

            await _repository.DeleteTaskAsync(task);

            return new Response("Tarefa removida com sucesso!", 201);
        }

        public async Task<Response> ListTask(Guid userId)
        {
            try
            {
                List<TaskEntity> tasks = await _repository.ListTasks(userId);
                
                foreach (var task in tasks)
                {
                    task.PhotoTask = string.Empty;
                }
                return new Response(string.Empty, tasks);
            }
            catch
            {
                return new Response("Erro ao listar as atividades!", 500);
            }
        }

        public async Task<Response> GetTask(Guid id)
        {
            try
            {
                var task = await _repository.GetTaskById(id);
                if (task == null)
                    return new Response($"Tarefa não encontrada! Id: {id}", 404);
                return new Response("Tarefa encontrada!", task);
            }
            catch (Exception ex)
            {
                return new Response($"Erro ao buscar a tarefa: {ex.Message}", 500);
            }

        }
    }
}
