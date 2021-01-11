using GomokuAdmin.Web.Models;
using GomokuAdmin.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GomokuAdmin.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private UserService _userService { get; }

        public UserController(UserService UserService)
        {
            _userService = UserService;
        }

        [HttpGet("[action]")]
        public IActionResult Search([FromQuery] string term = null)
        {
            return Json(_userService.Search(term));
        }

        //[HttpPatch("{id:int}")]
        //public IActionResult BanChat(Guid userId)
        //{
        //    if (userId == null)
        //        return BadRequest($"{nameof(userId)} is null.");
        //    var result = _userService.BanChat(userId);
        //    return Json(result);
        //}

    }
}