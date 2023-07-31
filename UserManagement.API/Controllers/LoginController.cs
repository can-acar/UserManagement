﻿using UserManagement.API.Requests;

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

    [HttpPost("", Name = "Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        _logger.LogInformation("Login request received with username: {Username}", request.Username);

        var token = await _mediator.Send(new LoginUserQuery(request.Username, request.Password));

        _logger.LogInformation("Login request completed successfully with username: {Username}", request.Username);

        return Ok(token);
    }
}