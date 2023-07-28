using UserManagement.API.Requests;

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

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
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

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
    {
        try
        {
            await _mediator.Send(new CreateUserCommand(request.Username, request.Password));

            return Ok("Registration successful. Check your email for activation link.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPost("profile/{userId}/register")]
    public async Task<IActionResult> RegisterProfile([FromBody] RegisterProfileRequest request, Guid userId)
    {
        try
        {
            if (userId != request.UserId)
            {
                return BadRequest("User Id mismatch.");
            }

            var result = await _mediator.Send(new RegisterUserProfileCommand(request.UserId, request.FirstName, request.LastName,
                request.Email, request.PhoneNumber, request.Address, request.City, request.State, request.ZipCode,
                request.Country, request.ProfilePictureUrl));

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPost("profile/update")]
    [Authorize] // Implement JWT authentication for this endpoint
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
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
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequest request)
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
    public async Task<IActionResult> DeactivateAccount([FromBody] DeactivateAccountRequest request)
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