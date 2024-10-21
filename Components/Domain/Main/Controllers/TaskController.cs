using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskList.Components.Domain.Main.DTOs.TaskDTOs;
using TaskList.Components.Domain.Main.Entities;
using TaskList.Components.Domain.Main.UseCases.Create;

namespace TaskList.Components.Domain.Main.Controllers
{
    [ApiController]
    [Authorize]
    [Route("task")]
    public class TaskController : Controller
    {
        private readonly TaskHandler _handler;
        public TaskController(TaskHandler handler)
        {
            _handler = handler;
        }

        [SwaggerOperation(Summary = "Criar tarefa.", Description = "Cria uma nova tarefa.")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateTask([FromBody] RequestTask newTask)
        {
            var result = await _handler.CreateTask(newTask);

            return Ok(result);
        }

        [SwaggerOperation(Summary = "Editar tarefa.", Description = "Endpoint para editar uma tarefa. " +
            "Pode editar título, descrição, data de início ou data de término.")]
        [HttpPost("edit")]
        public async Task<IActionResult> EditTask([FromBody] RequestTask taskToEdit)
        {
            var result = await _handler.EditTask(taskToEdit);

            return Ok(result);

        }

        [SwaggerOperation(Summary = "Remover tarefa.", Description = "Endpoint para apagar uma tarefa")]
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteTask([FromBody] RequestTask taskToDelete)
        {
            var result = await _handler.DeleteTask(taskToDelete.Id);

            return Ok(result);

        }

        [SwaggerOperation(Summary = "Listar tarefas.", Description = "Endpoint para listas todas as tarefas")]
        [HttpGet("list")]
        public async Task<IActionResult> ListTask()
        {
            var result = await _handler.ListTask();

            return Ok(result);

        }
        [SwaggerOperation(Summary = "Buscar uma tarefa.", Description = "Endpoint para buscar uma tarefa através do ID.")]
        [HttpGet("task/{taskId}")]
        public async Task<IActionResult> GetTask([FromRoute] Guid taskId)
        {
            var result = await _handler.GetTask(taskId);

            return Ok(result);

        }

    }
}
