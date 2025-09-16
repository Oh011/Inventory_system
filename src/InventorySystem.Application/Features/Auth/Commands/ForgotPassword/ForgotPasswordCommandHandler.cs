using Application.Features.Auth.Interfaces;
using MediatR;
using InventorySystem.Application.Features.Auth.Commands.PasswordRest;

namespace InventorySystem.Application.Features.Auth.Commands.ForgotPassword
{
    internal class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, string>
    {

        private readonly IAuthenticationService authenticationService;


        public ForgotPasswordCommandHandler(IAuthenticationService authenticationService)
        {

            this.authenticationService = authenticationService;
        }
        public async Task<string> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {


            var result = await authenticationService.ForgotPassword(request.Email);


            return result;
        }
    }
}
