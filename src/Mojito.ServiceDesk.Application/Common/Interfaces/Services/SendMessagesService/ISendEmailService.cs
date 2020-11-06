using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Application.Common.Interfaces.Services.SendMessagesService
{
    public interface ISendEmailService
    {
        void SendEmailAsync(string toEmail, string subject, string message, bool isMessageHtml = false);
    }
}
