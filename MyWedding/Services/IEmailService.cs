using System.Collections.Generic;
using System.Threading.Tasks;
namespace Email.Services
{
    public interface IEmailService
    {
        Task SendLogin(string email, string subject, string message);
        Task SendEmail(List<string> email, string subject, string message);
    }
}