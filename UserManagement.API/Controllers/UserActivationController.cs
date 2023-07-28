namespace UserManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserActivationController : ControllerBase
{
    private readonly ILogger<UserActivationController> _logger;
    private readonly IMediator _mediator;

    public UserActivationController(ILogger<UserActivationController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
}