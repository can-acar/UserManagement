using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly PasswordHasher<Users> _passwordHasher;
        private readonly PasswordHasher<UserActivations> _passwordHasherActivation;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _passwordHasherActivation = new PasswordHasher<UserActivations>();
            _passwordHasher = new PasswordHasher<Users>();
        }

        public async Task<(string, ServiceResponse)> CreateUser(CreateUserCommand user)
        {
            _logger.LogInformation("[EXECUTING]UserService.CreateUser: {Username},Detail:{@user}", user.Username, user);

            var hasUser = await _userRepository.HasUser(new Users
            {
                Username = user.Username,
                Email = user.Email
            });

            if (hasUser)
            {
                _logger.LogError("[FAILED]UserService.CreateUser: {Username},Detail:{@user}", user.Username, user);
                throw new AppException("Username or Email already exists.");
            }

            var hashedPassword = _passwordHasher.HashPassword(new Users(), user.Password);
            var activationCode = Guid.NewGuid().ToString();
            var hashedActivationCode = _passwordHasherActivation.HashPassword(new UserActivations(), activationCode);

            var tmpUser = new Users
            {
                Username = user.Username,
                Email = user.Email,
                Password = hashedPassword,
                UserActivations = new List<UserActivations>()
            };

            tmpUser.UserActivations.Add(new UserActivations
            {
                ActivationCode = hashedActivationCode,
                ExpirationDate = DateTime.Now.AddDays(1),
                CreatedAt = DateTime.Now
            });

            var newUser = await _userRepository.CreateUser(tmpUser);


            _logger.LogInformation("[EXECUTED:SUCCESS]UserService.CreateUser: {Username},Detail:{@user}", user.Username, newUser);

            return (hashedActivationCode, await ServiceResponse.SuccessAsync("User created successfully.", new
            {
                Username = newUser.Username,
                Email = newUser.Email
            }));
        }

        public Task UpdateProfile(Guid userId, string username, string email)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePassword(Guid userId, string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse> ActivateUser(string activationCode)
        {
            try
            {
                var hasActivationCode = await _userRepository.HasActivationCode(activationCode);

                var hasExpiredCode = await _userRepository.HasExpiredCode(activationCode);

                if (!hasActivationCode && hasExpiredCode)
                {
                    return await ServiceResponse.ErrorAsync("Activation code is invalid or expired.");
                }

                var query = await _userRepository.GetUserFromActivationCode(activationCode);

                if (query == null)
                {
                    return await ServiceResponse.ErrorAsync("Activation code is invalid or expired.");
                }


                query.User.IsActive = true;
                query.User.UpdatedAt = DateTime.Now;
                query.UpdatedAt = DateTime.Now;

                await _userRepository.BeginTransactionAsync();

                await _userRepository.UpdateUserStatus(query);

                await _userRepository.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[FAILED]UserService.ActivateUser: {ActivationCode},Detail:{@ex}", activationCode, ex);

                await _userRepository.RollbackAsync();
                throw;
            }

            return await ServiceResponse.ErrorAsync("Activation code is invalid or expired.");
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

        private string GenerateJwtToken(Users user)
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