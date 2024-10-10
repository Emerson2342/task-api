﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskList.Components.Domain.Main.DTOs;
using TaskList.Components.Domain.Main.UseCases.Create;

namespace TaskList.Components.Domain.Main.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly Handler _handler;

        public HomeController(Handler handler)
        {
            _handler = handler;
        }

        [HttpGet("/v1")]
        public IActionResult Get()
        {
          
            return Ok("result");
        }
    }
}
