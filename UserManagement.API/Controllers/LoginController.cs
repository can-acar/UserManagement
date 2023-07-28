namespace UserManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILogger<LoginController> _logger;
    private readonly IMediator _mediator;

    public LoginController(IMediator mediator, ILogger<LoginController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
}