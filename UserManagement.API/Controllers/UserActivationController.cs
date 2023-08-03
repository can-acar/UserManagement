using System.Text;
using System.Web;
using UserManagement.Core.Commands;

namespace UserManagement.API.Controllers
{
    [Controller]
    [Route("user-activation")]
    public class UserActivationController : ControllerBase
    {
        private readonly ILogger<UserActivationController> _logger;
        private readonly IMediator _mediator;

        public UserActivationController(ILogger<UserActivationController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("{activationCode}", Name = "ActivateUser")]
        public async Task<IActionResult> UserActivation([FromRoute] string activationCode)
        {
            try
            {
                var response = await _mediator.Send(new ActivateUserCommand(activationCode));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}