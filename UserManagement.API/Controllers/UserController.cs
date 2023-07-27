namespace UserManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<UserController> _logger;

    public UserController(IMediator mediator, ILogger<UserController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        try
        {
            await _mediator.Send(new RegisterUserCommand(request.Username, request.Email, request.Password));

            return Ok("Registration successful. Check your email for activation link.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        try
        {
            var token = await _mediator.Send(new LoginUserQuery(request.Username, request.Password));

            return Ok(new {AccessToken = token});
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("profile/update")]
    [Authorize] // Implement JWT authentication for this endpoint
    public async Task<IActionResult> UpdateProfile(UpdateProfileRequest request)
    {
        try
        {
            await _mediator.Send(new UpdateUserProfileCommand(request.UserId, request.Username, request.Email));
            return Ok("Profile updated successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("profile/update-password")]
    [Authorize] // Implement JWT authentication for this endpoint
    public async Task<IActionResult> UpdatePassword(UpdatePasswordRequest request)
    {
        try
        {
            await _mediator.Send(new UpdateUserPasswordCommand(request.UserId, request.NewPassword));
            
            return Ok("Password updated successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("profile/deactivate")]
    [Authorize] // Implement JWT authentication for this endpoint
    public async Task<IActionResult> DeactivateAccount(DeactivateAccountRequest request)
    {
        try
        {
            await _mediator.Send(new DeactivateUserAccountCommand(request.UserId));
            return Ok("Account deactivated successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}