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

public interface IMailProvider
{
    Task SendMail(string to, string mail, string subject, string body);
}


// const string smtpServer = "localhost";
// var smtpPort = 1025;
//
// // Mail gönderici bilgileri
// var senderEmail = "hesap@aktivasyonu.com";
// var senderPassword = "gonderen_sifre";
// var senderName = "Hesap Aktivasyonu";
//
// // Mail alıcı bilgileri
// var recipientEmail = "can.acar@windowslive.com";
// var recipientName = "Can ACAR";
//
// // Mail başlık ve içeriği
// var subject = "Hesap Aktivasyonu";
// var activationLink = $"https://localhost:7041/user-activation/{context.Message.AktifasyonKodu}";
// var htmlBody = GetActivationEmailTemplate(context.Message.Username, activationLink, "SoftRobotics");
//
// try
// {
//     // SMTP istemci oluşturma ve ayarlarını yapılandırma
//     using (var smtpClient = new SmtpClient(smtpServer, smtpPort))
//     {
//         smtpClient.UseDefaultCredentials = false;
//         smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
//         smtpClient.EnableSsl = false;
//
//         // Mail oluşturma ve gönderme
//         using (var mailMessage = new MailMessage())
//         {
//             mailMessage.From = new MailAddress(senderEmail, senderName);
//             mailMessage.To.Add(new MailAddress(recipientEmail, recipientName));
//             mailMessage.Subject = subject;
//             mailMessage.Body = htmlBody;
//             mailMessage.IsBodyHtml = true;
//             smtpClient.Send(mailMessage);
//         }
//     }
//
//     Console.WriteLine("E-posta başarıyla gönderildi.");
// }
// catch (Exception ex)
// {
//     _logger.LogError(ex, "Error while sending activation mail to {Email}", context.Message.Email);
//     Console.WriteLine("E-posta gönderirken bir hata oluştu: " + ex.Message);
// }