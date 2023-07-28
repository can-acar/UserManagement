namespace UserManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly ILogger<ProfileController> _logger;
    private readonly IMediator _mediator;

    public ProfileController(ILogger<ProfileController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
}