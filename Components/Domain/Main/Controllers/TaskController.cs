using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskList.Components.Domain.Main.DTOs.TaskDTOs;
using TaskList.Components.Domain.Main.UseCases.Create;

namespace TaskList.Components.Domain.Main.Controllers
{
    [ApiController]
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
        [HttpPost("remove")]
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

    }
}
