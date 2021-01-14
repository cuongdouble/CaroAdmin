using GomokuAdmin.Data;
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

        [HttpPatch("{id}")]
        public IActionResult Update(User model)
        {
            if (model == null)
                return BadRequest($"{nameof(model)} is null.");
            var result = _userService.Update(model);
            return Json(result);
        }

    }
}