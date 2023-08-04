using UserManagement.Core.Commands;
using UserManagement.Core.Services;

namespace UserManagement.MailService.Handler;

public class SendActivationMailHandler : IRequestHandler<ActivateUserAccountCommand, ServiceResponse>
{
    private readonly ILogger<SendActivationMailHandler> _logger;
    private readonly IMailProvider _mailProvider;
    private readonly IEmailRenderService _emailRenderService;
    private readonly IConfiguration _configuration;

    public SendActivationMailHandler(ILogger<SendActivationMailHandler> logger,
        IMailProvider mailProvider,
        IEmailRenderService emailRenderService, IConfiguration configuration)
    {
        _configuration = configuration;
        _logger = logger;
        _mailProvider = mailProvider;
        _emailRenderService = emailRenderService;
    }

    public async Task<ServiceResponse> Handle(ActivateUserAccountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"Sending activation mail to {request.Email}.");

            var url = _configuration["Activation:Url"];
            
            var activationLink = $"{url}/{request.ActivationCode}";

            var body = await _emailRenderService.RenderEmailTemplate(request.Username, activationLink, "SoftRobotics");

            await _mailProvider.SendMail(to: request.Username, mail: request.Email, subject: "", body: body);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while sending activation mail to {Email}", request.Email);
        }

        return await ServiceResponse.SuccessAsync("Activation Mail Sent Successfully");
    }
}

// Send Update Mail Handler