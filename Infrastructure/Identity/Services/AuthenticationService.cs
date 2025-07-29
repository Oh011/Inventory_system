using Application.Features.Auth.Dtos;
using Application.Features.Auth.Interfaces;
using Application.Features.Auth.Results;
using Application.Exceptions;
using Microsoft.AspNetCore.Identity;
using Project.Application.Common.Interfaces;
using Project.Application.Common.Interfaces.Services;
using Project.Application.Features.Auth.Commands.ResetPassword;
using Shared.Dtos;
using IAuthenticationService = Application.Features.Auth.Interfaces.IAuthenticationService;



namespace Infrastructure.Identity.Services
{
    internal class AuthenticationService : IAuthenticationService
    {


        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ITokenService _tokenService;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly ILinkBuilder linkBuilder;


        private readonly IEmailService emailService;

        public AuthenticationService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            ITokenService tokenService, ILinkBuilder linkBuilder, IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            this.linkBuilder = linkBuilder;
            this.emailService = emailService;
        }

        public async Task<AuthResultWithRefreshToken> LogIn(string email, string password, string DeviceId)
        {


            var user = await _userManager.FindByEmailAsync(email);



            if (user == null)
            {

                throw new UnAuthorizedException("Invalid credentials");
            }

            if (await _userManager.IsLockedOutAsync(user))
            {

                throw new AccountLockedException();
            }


            var result = await _userManager.CheckPasswordAsync(user, password);


            if (!result)
            {
                // Increment failed login count
                await _userManager.AccessFailedAsync(user);

                // Check if user is locked out now
                if (await _userManager.IsLockedOutAsync(user))
                {
                    throw new AccountLockedException();
                }

                throw new UnAuthorizedException("Invalid credentials.");
            }


            await _userManager.ResetAccessFailedCountAsync(user);


            var userRoles = await _userManager.GetRolesAsync(user);




            var accessToken = _tokenService.GenerateAccessToken(user.UserName, user.Email, user.Id,
          userRoles);


            var refreshToken = await _tokenService.CreateRefreshTokenForDevice(user.Id, DeviceId);




            return new AuthResultWithRefreshToken
            {
                Response = new LogInUserResponseDto
                {
                    AccessToken = accessToken,
                    AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(60),
                    Email = user.Email,
                    FullName = user.FullName,
                    Role = userRoles.FirstOrDefault()

                },
                RefreshToken = refreshToken
            };
        }

        public Task<string> LogOut(string token, string deviceId)
        {



            return _tokenService.RevokeRefreshTokenByToken(token, deviceId);
        }





        public async Task<string> ForgotPassword(string email)
        {


            var user = await _userManager.FindByEmailAsync(email);


            if (user == null)
                return "If an account with that email exists, a password reset link has been sent.";


            var token = await _userManager.GeneratePasswordResetTokenAsync(user);


            var link = linkBuilder.BuildPasswordResetLink(email, token);


            var emailMessage = new EmailMessage
            {
                Subject = "Password Reset",
                Body = $"Reset your password using this link: {link}",
                To = email,
            };

            await emailService.SendEmailAsync(emailMessage);
            return "If an account with that email exists, a password reset link has been sent.";



        }


        public async Task<string> RestPassword(RestPasswordCommand request)
        {


            var user = await _userManager.FindByEmailAsync(request.Email);


            if (user == null)
                throw new NotFoundException("Invalid email.");




            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);


            if (!result.Succeeded)
            {
                var errors = result.Errors
                    .GroupBy(e => e.Code)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.Description).ToList());

                throw new ValidationException(errors);
            }


            return "Password has been reset successfully.";
        }




    }




}
