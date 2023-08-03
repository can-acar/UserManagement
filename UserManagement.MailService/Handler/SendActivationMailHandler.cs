using UserManagement.Core.Commands;
using UserManagement.Core.Services;

namespace UserManagement.MailService.Handler;

public class SendActivationMailHandler : IRequestHandler<ActivateUserAccountCommand, ServiceResponse>
{
    private readonly ILogger<SendActivationMailHandler> _logger;
    private readonly IMailProvider _mailProvider;
    private readonly IEmailRenderService _emailRenderService;

    public SendActivationMailHandler(ILogger<SendActivationMailHandler> logger, IMailProvider mailProvider, IEmailRenderService emailRenderService)
    {
        _logger = logger;
        _mailProvider = mailProvider;
        _emailRenderService = emailRenderService;
    }

    public async Task<ServiceResponse> Handle(ActivateUserAccountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"Sending activation mail to {request.Email}.");


            var activationLink = $"https://localhost:7041/user-activation/{request.ActivationCode}";

            var body = await _emailRenderService.RenderEmailTemplate(request.Username, activationLink, "SoftRobotics");

        
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while sending activation mail to {Email}", request.Email);
        }

        return await ServiceResponse.SuccessAsync("Activation Mail Sent Successfully");
    }
}

// Send Update Mail Handler