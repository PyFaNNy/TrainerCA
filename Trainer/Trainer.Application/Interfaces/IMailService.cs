using Trainer.Application.Models.Email;

namespace Trainer.Application.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
