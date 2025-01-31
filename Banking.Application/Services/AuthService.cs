using Banking.Application.Contracts.Interfaces;
using Banking.Application.Models.Dto;
using Banking.Domain.Models;
using Banking.Domain.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Banking.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUserRepository userRepository, IConfiguration configuration, IPasswordHasher<User> passwordHasher, 
            ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        public async Task<ResponseViewModel<string>> AuthenticateAsync(LoginDto login)
        {
            var response = new ResponseViewModel<string>();
            _logger.LogInformation("user authentication is started.");

            try
            {
                var user = await _userRepository.GetUserByUserName(login.Username);
                if (user == null)
                {
                    response.Message = "Invalid username or password";
                    _logger.LogError("Invalid username or password");
                    return response;
                }

                var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, login.Password);

                if (verificationResult == PasswordVerificationResult.Success)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Issuer  = _configuration["Jwt:Issuer"],
                        Audience = _configuration["Jwt:Audience"],
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, user.Username),
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                        }),
                        Expires = DateTime.UtcNow.AddHours(1), 
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    response.Success = true;
                    response.Data = tokenHandler.WriteToken(token);
                    response.Message = "User authenticated successfully";
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Some Error Occured: {message}", ex.Message);
                response.Message = "Some Error Occured";
            }
            return response;
        }
    }

}
