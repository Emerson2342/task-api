using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using TaskList.Components.Domain.Main.DTOs.TaskDTOs;
using TaskList.Components.Domain.Main.Entities;
using TaskList.Components.Domain.Main.UseCases.Create;
using TaskList.Components.Domain.Main.UseCases.ResponseCase;
using TaskList.Components.Pages.TaskPages;

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
        public async Task<IActionResult> CreateTask([FromForm] RequestTask newTask)
        {
            string photoBase64 = string.Empty;

            if (newTask.PhotoFile != null)
            {
                using var memoryStream = new MemoryStream();
      
                await newTask.PhotoFile.CopyToAsync(memoryStream);
          
                photoBase64 = Convert.ToBase64String(memoryStream.ToArray());

                newTask.PhotoTask = photoBase64;
            }

            var result = await _handler.CreateTask(newTask);
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Editar tarefa.", Description = "Endpoint para editar uma tarefa. " +
            "Pode editar título, descrição, data de início ou data de término.")]
        [HttpPost("edit")]
        public async Task<IActionResult> EditTask([FromForm] RequestTask taskToEdit)
        {
            string photoBase64 = string.Empty;

            if (taskToEdit.PhotoFile != null)
            {
                using var memoryStream = new MemoryStream();

                await taskToEdit.PhotoFile.CopyToAsync(memoryStream);

                photoBase64 = Convert.ToBase64String(memoryStream.ToArray());

                taskToEdit.PhotoTask = photoBase64;
            }

            var result = await _handler.EditTask(taskToEdit);

            return Ok(result);

        }

        [SwaggerOperation(Summary = "Recupera foto da tarefa.", Description = "Endpoint para recuperar foto da tarefa")]
        [HttpGet("get-photo/{taskId}")]
        public async Task<IActionResult> GetPhotoFile([FromRoute] Guid taskId)
        {
            var result = await _handler.GetTask(taskId);
            var base64 = result.TaskList?.PhotoTask.ToString();
            if (string.IsNullOrEmpty(base64))
                return NotFound();
            var fileBytes = Convert.FromBase64String(base64);
            return File(fileBytes, "image/jpeg", $"{taskId}.jpg");
        }

        [SwaggerOperation(Summary = "Remover tarefa.", Description = "Endpoint para apagar uma tarefa")]
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteTask([FromBody] RequestTask taskToDelete)
        {
            var result = await _handler.DeleteTask(taskToDelete.Id);
            return Ok(result);

        }

        [SwaggerOperation(Summary = "Listar tarefas.", Description = "Endpoint para listas todas as tarefas")]
        [HttpGet("list/{userId}")]
        public async Task<IActionResult> ListTask([FromRoute] Guid userId)
        {
            var tasks = await _handler.ListTask(userId);

            return Ok(tasks);

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
