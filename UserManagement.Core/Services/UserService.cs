using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using UserManagement.Core.Commands;
using UserManagement.Core.Models;
using UserManagement.Core.Repositories;
using UserManagement.Infrastructure.Exceptions;

namespace UserManagement.Core.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<ServiceResponse> CreateUser(CreateUserCommand user)
        {
            _logger.LogInformation("[EXECUTING]UserService.CreateUser: {Username},Detail:{@user}", user.Username, user);

            var hasUser = await _userRepository.HasUser(new User
            {
                Username = user.Username,
                Email = user.Email
            });

            if (hasUser)
            {
                _logger.LogError("[FAILED]UserService.CreateUser: {Username},Detail:{@user}", user.Username, user);
                throw new AppException("Username or Email already exists.");
            }

            var hashedPassword = _passwordHasher.HashPassword(new User(), user.Password);

            var newUser = await _userRepository.CreateUser(new User
            {
                Username = user.Username,
                Email = user.Email,
                Password = hashedPassword
            });

            _logger.LogInformation("[EXECUTED:SUCCESS]UserService.CreateUser: {Username},Detail:{@user}", user.Username, newUser);

            return await ServiceResponse.SuccessAsync("User created successfully.", new
            {
                Username = newUser.Username,
                Email = newUser.Email
            });
        }

        public Task UpdateProfile(Guid userId, string username, string email)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePassword(Guid userId, string newPassword)
        {
            throw new NotImplementedException();
        }

        // private string GenerateAccessToken(string username)
        // {
        //     // Implement your JWT generation logic here
        //     // Example using System.IdentityModel.Tokens.Jwt:
        //     var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Your_Secret_Key_Here"));
        //     var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        //     var claims = new[]
        //     {
        //         new Claim(ClaimTypes.Name, username)
        //         // Add more claims as needed
        //     };
        //
        //     var token = new JwtSecurityToken(
        //         issuer: "Your_Issuer",
        //         audience: "Your_Audience",
        //         claims: claims,
        //         expires: DateTime.UtcNow.AddMinutes(30), // Set the token expiration time
        //         signingCredentials: credentials
        //     );
        //
        //     return new JwtSecurityTokenHandler().WriteToken(token);
        // }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key-here"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "user")
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: "your-issuer-here",
                audience: "your-audience-here",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return token;
        }
    }
}