using Microsoft.Extensions.Configuration;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.SendMessagesService;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Infrastructure.Services.SendMessagesService
{
    public class SendEmailService : ISendEmailService
    {
        #region ctor
        private readonly IConfiguration configuration;

        public SendEmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        #endregion

        public async void SendEmailAsync(string toEmail, string subject, string message, bool isMessageHtml = false)
        {
            await Task.Run(() =>
            {
                using (var client = new SmtpClient())
                {
                    //todo: use IOptions
                    var username = configuration
                        .GetSection("EmailConfig")
                        .GetSection("UserName").Value;
                    var password = configuration
                        .GetSection("EmailConfig")
                        .GetSection("Password").Value;
                    var emailDomain = configuration
                        .GetSection("EmailConfig")
                        .GetSection("Domain").Value;
                    var emailHost = configuration
                        .GetSection("EmailConfig")
                        .GetSection("Host").Value;
                    var emailPort = int.Parse(configuration
                        .GetSection("EmailConfig")
                        .GetSection("Port").Value);
                    var enableSSL = bool.Parse(configuration
                        .GetSection("EmailConfig")
                        .GetSection("EnableSSL").Value);

                    var credentials = new NetworkCredential()
                    {
                        UserName = username,
                        Password = password
                    };

                    client.UseDefaultCredentials = false;
                    client.Credentials = credentials;
                    client.Host = emailHost;
                    client.Port = emailPort;
                    client.EnableSsl = enableSSL;
                    using var emailMessage = new MailMessage()
                    {
                        To = { new MailAddress(toEmail) },
                        From = new MailAddress(new StringBuilder(username)
                            .Append("@")
                            .Append(emailDomain).ToString()),
                        Subject = subject,
                        Body = message,
                        IsBodyHtml = isMessageHtml
                    };

                    client.Send(emailMessage);
                }
            });

        }
    }
}
