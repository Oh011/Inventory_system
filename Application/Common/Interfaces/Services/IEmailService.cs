using Shared.Dtos;

namespace Project.Application.Common.Interfaces.Services
{
    public interface IEmailService
    {


        Task SendEmailAsync(EmailMessage email);
    }
}
