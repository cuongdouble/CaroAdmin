using GomokuAdmin.Data;
using GomokuAdmin.Web.Models;
using GomokuAdmin.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GomokuAdmin.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private GameService _gameService { get; }

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("[action]")]
        public IActionResult GetAll()
        {
            return Json(_gameService.GetAll());
        }

        [HttpGet("[action]")]
        public IActionResult Search(Guid? id)
        {
            return Json(_gameService.Search(id));
        }

        [HttpGet("[action]")]
        public IActionResult GetChat(Guid id)
        {
            return Json(_gameService.GetChat(id));
        }

    }

}