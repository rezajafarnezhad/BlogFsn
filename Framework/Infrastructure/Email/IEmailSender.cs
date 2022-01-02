using System.Threading.Tasks;

namespace Framework.Infrastructure.Email
{
    public interface IEmailSender
    {
        Task Send(string userEmail, string body, string subject);
    }
}