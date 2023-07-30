﻿using UserManagement.API.Requests;

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

    [HttpPost("", Name = "ActivateUser")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var response = await _mediator.Send(new LoginUserQuery(request.Username, request.Password));

            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}