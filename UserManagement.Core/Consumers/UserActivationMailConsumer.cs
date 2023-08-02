using System.Net;
using System.Net.Mail;
using MediatR;
using UserManagement.Core.Commands;
using UserManagement.Core.Interfaces;

namespace UserManagement.Core.Consumers
{
    public class UserActivationMailConsumer : IConsumer<IUserRegisterActivateMailSendEvent>
    {
        private readonly ILogger<UserActivationMailConsumer> _logger;
        private readonly IMediator _mediator;

        public UserActivationMailConsumer(ILogger<UserActivationMailConsumer> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }


        public async Task Consume(ConsumeContext<IUserRegisterActivateMailSendEvent> context)
        {
            _logger.LogInformation("Sending activation mail to {Email}", context.Message.Email);

            await _mediator.Send(new ActivateUserAccountCommand(context.Message.Email, context.Message.Username, context.Message.ActivationCode));


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

            await Task.CompletedTask;
        }
    }
}