using UserManagement.API.Repositories;
using UserManagement.Core.Models;
using UserManagement.Infrastructure.Commons;
using UserManagement.Infrastructure.Exceptions;

namespace UserManagement.API.Services;

internal class UserService : IUserService
{
    private readonly ILogger<UserService> _Logger;
    private readonly IUserRepository _userRepository;

    public UserService(ILogger<UserService> logger, IUserRepository userRepository)
    {
        _Logger = logger;
        _userRepository = userRepository;
    }

    public async Task<ServiceResponse> CreateUser(CreateUserCommand user)
    {
        _Logger.LogInformation("[EXECUTING]UserService.CreateUser: {Username},Detail:{@user}", user.Username, user);

        var hasUser = await _userRepository.HasUser(new User
        {
            Username = user.Username,
            Email = user.Email
        });

        if (hasUser)
        {
            _Logger.LogError("[FAILED]UserService.CreateUser: {Username},Detail:{@user}", user.Username, user);
            throw new AppException("Username or Email already exists.");
        }

        var newUser = await _userRepository.CreateUser(new User
        {
            Username = user.Username,
            Email = user.Email,
            Password = user.Password
        });

        _Logger.LogInformation("[EXECUTED:SUCCESS]UserService.CreateUser: {Username},Detail:{@user}", user.Username, newUser);

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
}