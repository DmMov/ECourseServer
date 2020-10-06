using SendGrid;
using System.Threading.Tasks;

namespace ECourse.Application.Interfaces
{
    public interface IMailSenderService
    {
        public Task SendEmailAsync(string email, string subject, string message);
        public Task<Response> Execute(string apiKey, string subject, string message, string email);
    }
}