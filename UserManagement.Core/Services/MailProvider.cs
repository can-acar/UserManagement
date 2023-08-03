using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace UserManagement.Core.Services;

public class MailProvider : IMailProvider
{
    private readonly ILogger<MailProvider> _logger;
    private readonly IConfiguration _configuration;
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _smtpUser;
    private readonly string _smtpPassword;
    private readonly string _smtpFrom;

    public MailProvider(ILogger<MailProvider> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        _smtpServer = _configuration["Mail:SmtpServer"];
        _smtpPort = Convert.ToInt32(_configuration["Mail:SmtpPort"]);
        _smtpUser = _configuration["Mail:SmtpUser"];
        _smtpFrom = _configuration["Mail:SmtpFrom"];
        _smtpPassword = _configuration["Mail:SmtpPassword"];
    }


    public Task SendMail(string to, string mail, string subject, string body)
    {
        try
        {
            _logger.LogInformation("Mail gönderiliyor.");

            using var client = new SmtpClient(_smtpServer, _smtpPort);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_smtpUser, _smtpPassword);
            client.EnableSsl = false;

            using var message = new MailMessage();
            message.From = new MailAddress(_smtpFrom, _smtpUser);
            message.To.Add(new MailAddress(mail, to));
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            client.Send(message);

            _logger.LogInformation("Mail gönderildi.");
        }
        catch (Exception e)
        {
            _logger.LogError("Mail gönderilirken hata oluştu. Hata: {0}", e.Message);
        }

        return Task.CompletedTask;
    }
}