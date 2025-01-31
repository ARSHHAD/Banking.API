using Banking.Application.Contracts.Interfaces;
using Banking.Application.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Banking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginModel)
        {
            _logger.LogInformation("Start login request");

            var response = await _authService.AuthenticateAsync(loginModel);

            if (!response.Success)
            {
                return Unauthorized(response);
            }
            return Ok(response);
        }
            
    }
}
