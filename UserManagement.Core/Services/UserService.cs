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
        private readonly ITokenService _tokenService;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository, ITokenService tokenService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _tokenService = tokenService;
            // _passwordHasherActivation = new PasswordHasher<UserActivations>();
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
            var hashedActivationCode = _tokenService.GenerateToken(activationCode); //_passwordHasherActivation.HashPassword(new UserActivations(), activationCode);

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


        public async Task<ServiceResponse> UpdatePassword(Guid userId, string password)
        {
            try
            {
                _logger.LogInformation("[EXECUTING]UserService.UpdatePassword: {UserId},Detail:{@user}", userId, password);

                var user = await _userRepository.GetUser(p => p.Id == userId);

                if (user == null)
                {
                    _logger.LogError("[FAILED]UserService.UpdatePassword: {UserId},Detail:{@user}", userId, password);
                    throw new AppException("User not found.");
                }

                var hashedPassword = _passwordHasher.HashPassword(user, password);

                user.Password = hashedPassword;

                await _userRepository.BeginTransactionAsync();

                await _userRepository.UpdateUser(user);

                await _userRepository.CommitAsync();

                _logger.LogInformation("[EXECUTED:SUCCESS]UserService.UpdatePassword: {UserId},Detail:{@user}", userId, password);

                return await ServiceResponse.SuccessAsync("Password updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError("[FAILED]UserService.UpdatePassword: {UserId},Detail:{@ex}", userId, ex);

                await _userRepository.RollbackAsync();

                throw;
            }
        }

        public async Task<ServiceResponse> ActivateUser(string activationCode)
        {
            try
            {
                var isActivate = await _userRepository.IsUserActivate(activationCode);

                if (isActivate)
                {
                    return await ServiceResponse.ErrorAsync("User already activated.");
                }

                var hasActivationCode = await _userRepository.HasActivationCode(activationCode);

                var hasExpiredCode = await _userRepository.HasExpiredCode(activationCode);

                if (!hasActivationCode && !hasExpiredCode)
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

                return await ServiceResponse.SuccessAsync("User activated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError("[FAILED]UserService.ActivateUser: {ActivationCode},Detail:{@ex}", activationCode, ex);

                await _userRepository.RollbackAsync();

                throw;
            }
        }

        public async Task<ServiceResponse> DeactivateUser(Guid userId)
        {
            try
            {
                _logger.LogInformation("[EXECUTING]UserService.DeactivateUser: {UserId},Detail:{@user}", userId);

                var user = await _userRepository.GetUser(p => p.Id == userId);

                if (user == null)
                {
                    _logger.LogError("[FAILED]UserService.DeactivateUser: {UserId},Detail:{@user}", userId);
                    throw new AppException("User not found.");
                }

                user.IsActive = false;
                user.IsDeleted = true;
                user.UpdatedAt = DateTime.Now;

                await _userRepository.BeginTransactionAsync();

                await _userRepository.UpdateUser(user);

                await _userRepository.CommitAsync();

                _logger.LogInformation("[EXECUTED:SUCCESS]UserService.DeactivateUser: {UserId},Detail:{@user}", userId);

                return await ServiceResponse.SuccessAsync("User deactivated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError("[FAILED]UserService.DeactivateUser: {UserId},Detail:{@ex}", userId, ex);

                await _userRepository.RollbackAsync();

                throw;
            }
        }

        public async Task<ServiceResponse> UpdateUser(Guid userId, string userName, string email)
        {
            try
            {
                _logger.LogInformation("[EXECUTING]UserService.UpdateUser: {UserId},Detail:{@user}", userId, new
                {
                    UserId = userId,
                    Username = userName,
                    Email = email
                });

                var user = await _userRepository.GetUser(p => p.Id == userId);

                if (user == null)
                {
                    return await ServiceResponse.ErrorAsync("User not found.");
                }

                user.Username = userName;
                user.Email = email;
                user.UpdatedAt = DateTime.Now;

                await _userRepository.BeginTransactionAsync();

                await _userRepository.UpdateUser(user);

                await _userRepository.CommitAsync();

                return await ServiceResponse.SuccessAsync("User updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError("[FAILED]UserService.UpdateUser: {UserId},Detail:{@ex}", userId, ex);

                return await ServiceResponse.ErrorAsync("User update failed.");
            }
        }
    }
}