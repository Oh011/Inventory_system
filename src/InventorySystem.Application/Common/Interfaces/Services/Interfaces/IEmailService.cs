using Shared.Dtos;

namespace InventorySystem.Application.Common.Interfaces.Services.Interfaces
{
    public interface IEmailService
    {


        Task SendEmailAsync(EmailMessage email);
    }
}
