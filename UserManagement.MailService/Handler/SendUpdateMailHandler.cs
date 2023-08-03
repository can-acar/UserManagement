using UserManagement.Core.Commands;
using UserManagement.Core.Services;

namespace UserManagement.MailService.Handler;

public class SendUpdateMailHandler : IRequestHandler<UpdateUserAccountCommand, ServiceResponse>
{
    private readonly ILogger<SendUpdateMailHandler> _logger;
    private readonly IMailProvider _mailProvider;
    private readonly IEmailRenderService _emailRenderService;

    public SendUpdateMailHandler(ILogger<SendUpdateMailHandler> logger, IMailProvider mailProvider, IEmailRenderService emailRenderService)
    {
        _logger = logger;
        _mailProvider = mailProvider;
        _emailRenderService = emailRenderService;
    }

    public async Task<ServiceResponse> Handle(UpdateUserAccountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"Sending update mail to {request.Email}.");


            var body = await _emailRenderService.RenderUpdateEmailTemplate(request.Username, "SoftRobotics");

            await _mailProvider.SendMail(to: request.Username, mail: request.Email, subject: "", body: body);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while sending update mail to {Email}", request.Email);
        }

        return await ServiceResponse.SuccessAsync("Update Mail Sent Successfully");
    }
}