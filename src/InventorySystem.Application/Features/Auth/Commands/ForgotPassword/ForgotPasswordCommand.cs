using MediatR;

namespace InventorySystem.Application.Features.Auth.Commands.PasswordRest
{
    public class ForgotPasswordCommand : IRequest<string>
    {

        public string Email { get; set; }
    }
}
