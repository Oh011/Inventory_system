using Application.Features.Auth.Interfaces;
using MediatR;

namespace Project.Application.Features.Auth.Commands.ResetPassword
{
    internal class ResetPasswordHandler : IRequestHandler<RestPasswordCommand, string>
    {

        private readonly IAuthenticationService authenticationService;




        public ResetPasswordHandler(IAuthenticationService authenticationService)
        {

            this.authenticationService = authenticationService;
        }



        public async Task<string> Handle(RestPasswordCommand request, CancellationToken cancellationToken)
        {


            var result = await authenticationService.RestPassword(request);


            return result;
        }
    }
}
