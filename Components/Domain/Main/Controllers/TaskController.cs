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

        [SwaggerOperation(Summary = "Criar tarefa.", Description = "Cria uam nova tarefa.")]
        [HttpPost("create")]
       public async Task<IActionResult> CreateTask([FromBody] RequestCreateTask newTask)
        {
           var result = await  _handler.CreateTask(newTask);

            return Ok(result);
        }

    }
}
