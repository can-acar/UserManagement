using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserManagement.Core.Models;
using UserManagement.Core.Repositories;
using UserManagement.Infrastructure.Exceptions;

namespace UserManagement.Core.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly PasswordHasher<Users> _passwordHasher;
        private ILogger<IdentityService> _logger;
        private IUserRepository _userRepository;
        private ITokenService _tokenService;
        private string _secretKey;
        private readonly int _tokenExpirationMinutes;

        public IdentityService(IUserRepository userRepository,
            IConfiguration configuration,
            ILogger<IdentityService> logger,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _logger = logger;
            _tokenService = tokenService;
            _passwordHasher = new PasswordHasher<Users>();
            _secretKey = configuration.GetValue<string>("SecretKey");
            _tokenExpirationMinutes = configuration.GetValue<int>("TokenExpirationMinutes");
        }

        public async Task<ServiceResponse> Login(string username, string password)
        {
            
            var currentDateTime = DateTime.UtcNow;
            var user = await _userRepository.GetUser(p => p.Username == username);
            

            if (user == null)
            {
                _logger.LogError("[FAILED]IdentityService.Login: {Username}", username);
                throw new AuthenticationException("Username or password is incorrect.");
            }
            
            if (user.IsActive == false)
            {
                _logger.LogError("[FAILED]IdentityService.Login: {Username}", username);
                throw new AuthenticationException("User is not active.");
            }

            var result = _passwordHasher.VerifyHashedPassword(new Users(), user.Password, password);

            if (result == PasswordVerificationResult.Failed)
            {
                _logger.LogError("[FAILED]IdentityService.Login: {Username}", username);
                throw new AuthenticationException("Username or password is incorrect.");
            }

            var token = _tokenService.GenerateToken(user.Username);

            await _userRepository.BeginTransactionAsync();

            user.UserTokens.Add(new UserToken
            {
                UserId = user.Id,
                Token = token,
                CreatedAt = currentDateTime,
                ExpiredAt = currentDateTime.AddMinutes(_tokenExpirationMinutes)
            });

            await _userRepository.UpdateUser(user);

            await _userRepository.CommitAsync();

            _logger.LogInformation("[SUCCESS]IdentityService.Login: {Username}", username);

            return await ServiceResponse.SuccessAsync("Login successful.", new
            {
                Username = user.Username,
                Token = token,
                ExpiredAt = currentDateTime.AddMinutes(_tokenExpirationMinutes)
            });
        }


        public async Task<ServiceResponse> ForgotPassword(string email)
        {
            try
            {
                var user = await _userRepository.GetUser(p => p.Email == email);

                if (user == null)
                {
                    throw new AppException("Email is not registered.");
                }

                // TODO: Send email to user with password reset link

                return await ServiceResponse.SuccessAsync("Forgot password successful.");
            }
            catch (Exception ex)
            {
                return await ServiceResponse.ErrorAsync(ex.Message);
            }
        }
    }
}