using Banking.Application.Contracts.Interfaces;
using Banking.Application.Models.Dto;
using Banking.Domain.Models;
using Banking.Domain.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using static Banking.Domain.Enums.Enum;


namespace Banking.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        public async Task<ResponseViewModel<bool>> RegisterAsync(RegisterDto registerModel)
        {
            var response = new ResponseViewModel<bool>();
            _logger.LogInformation("User registration started");

            try
            {
                var existingUser = await _userRepository.GetUserByUserName(registerModel.Username);
                if (existingUser != null)
                {
                    response.Message = "Username is already taken.";
                    _logger.LogWarning("Username is already taken : {username}", registerModel.Username);
                    return response;
                }

                var existingEmail = await _userRepository.GetUserByEmail(registerModel.Email);
                if (existingEmail != null)
                {
                    response.Message = "Email is already registered.";
                    _logger.LogWarning("Email is already registered : {email}", registerModel.Email);
                    return response;
                }

                var hashedPassword = _passwordHasher.HashPassword(null, registerModel.Password);

                var newUser = new User
                {
                    Id = Guid.NewGuid(),
                    Username = registerModel.Username,
                    Password = hashedPassword,
                    Email = registerModel.Email,
                    Role = UserRole.Customer,
                    CreatedAt = DateTime.UtcNow
                };

                await _userRepository.AddUser(newUser);
                response.Success = true;
                response.Message = "User registered successfully";
                _logger.LogInformation("User registered successfully: {username}", registerModel.Username);
            }
            catch (Exception ex)
            {
                response.Message = "User registration failed";
                _logger.LogError("User registration failed: {message}", ex.Message);

            }

            return response; ;
        }

        public async Task<ResponseViewModel<User>> GetUserInfo(string userId)
        {
            var response = new ResponseViewModel<User>();
            try
            {
                response.Data = await _userRepository.GetUserById(new Guid(userId));
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "An Error Occured while fetching user info";
                _logger.LogError("An Error Occured while fetching user info: {message}", ex.Message);
            }

            return response;
        }

    }
}
