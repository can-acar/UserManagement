using UserManagement.Core.Commands;
using UserManagement.Core.Services;

namespace UserManagement.MailService.Handler;

public class SendActivationMailHandler : IRequestHandler<ActivateUserAccountCommand, ServiceResponse>
{
    private readonly ILogger<SendActivationMailHandler> _logger;
    private readonly IMailProvider _mailProvider;

    public SendActivationMailHandler(ILogger<SendActivationMailHandler> logger, IMailProvider mailProvider)
    {
        _logger = logger;
        _mailProvider = mailProvider;
    }

    public async Task<ServiceResponse> Handle(ActivateUserAccountCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new ServiceResponse());
    }
}