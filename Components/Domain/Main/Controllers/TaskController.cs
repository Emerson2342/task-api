using Microsoft.AspNetCore.Mvc;

namespace TaskList.Components.Domain.Main.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
