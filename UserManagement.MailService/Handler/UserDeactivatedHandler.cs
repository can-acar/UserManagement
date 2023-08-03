using UserManagement.Core.Commands;
using UserManagement.Core.Services;

namespace UserManagement.MailService.Handler;

public class UserDeactivatedHandler : IRequestHandler<DeactivateUserCommand, ServiceResponse>
{
    private readonly ILogger<UserDeactivatedHandler> _logger;
    private readonly IMailProvider _mailProvider;
    private readonly IEmailRenderService _emailRenderService;

    public UserDeactivatedHandler(ILogger<UserDeactivatedHandler> logger, IMailProvider mailProvider, IEmailRenderService emailRenderService)
    {
        _logger = logger;
        _mailProvider = mailProvider;
        _emailRenderService = emailRenderService;
    }

    public async Task<ServiceResponse> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"Sending deactivation mail to {request.UserId}.");

            var body = await _emailRenderService.RenderDeactivationEmailTemplate(request.Username, "SoftRobotics");

            await _mailProvider.SendMail(to: request.Username, mail: request.Email, subject: "", body: body);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while sending deactivation mail to {UserId}", request.UserId);
        }

        return await ServiceResponse.SuccessAsync("Deactivation Mail Sent Successfully");
    }
}