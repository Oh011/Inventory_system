using Application.Features.Auth.Results;
using Project.Application.Features.Auth.Commands.ResetPassword;

namespace Application.Features.Auth.Interfaces
{
    public interface IAuthenticationService
    {



        Task<string> ForgotPassword(string email);


        Task<string> RestPassword(RestPasswordCommand command);
        Task<string> LogOut(string token, string deviceId);
        Task<AuthResultWithRefreshToken> LogIn(string email, string password, string DeviceId);
    }
}
