using UserManagement.API.Requests;
using UserManagement.Core.Commands;

namespace UserManagement.API.Controllers
{
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
        {
            _logger.LogInformation("Register request received with username: {Username}, Detail:{@request}", request.Username, request);

            var result = await _mediator.Send(new CreateUserCommand(request.Username, request.Password, request.Email));

            _logger.LogInformation("Register request completed successfully with username: {Username}", request.Username);

            return Ok(result);
        }


        [HttpPost("update")]
        [Authorize] // Implement JWT authentication for this endpoint
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserRequest request)
        {
            try
            {
                var result = await _mediator.Send(new UpdateUserCommand(request.UserId, request.Username, request.Email));

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update-password")]
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

        [HttpPost("deactivate")]
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
}