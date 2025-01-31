using Azure;
using Banking.Application.Contracts.Interfaces;
using Banking.Application.Models.Dto;
using Banking.Domain.Models;
using Banking.Domain.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Banking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerModel)
        {
            _logger.LogInformation("started user registration.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _userService.RegisterAsync(registerModel);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);

        }

        [Authorize]
        [HttpGet("userinfo")]
        public async Task<IActionResult> UserInfo()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            _logger.LogInformation("started fetching user info for {user id}:", userId);

            var response = await _userService.GetUserInfo(userId);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


    }
}
