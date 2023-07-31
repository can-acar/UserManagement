using UserManagement.API.Requests;

namespace UserManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ForgotPasswordController : ControllerBase
{
    private readonly ILogger<ForgotPasswordController> _logger;
    private readonly IMediator _mediator;

    public ForgotPasswordController(ILogger<ForgotPasswordController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost("", Name = "ForgotPassword")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        _logger.LogInformation("Forgot password request received with email: {Email}", request.Email);

        var result = await _mediator.Send(new ForgotPasswordCommand(request.Email));

        _logger.LogInformation("Forgot password request completed successfully with email: {Email}", request.Email);

        return Ok(result);
    }
}