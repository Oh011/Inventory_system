using Microsoft.Extensions.Options;
using Project.Application.Common.Interfaces;
using Shared.Options;
using System.Net;

namespace Infrastructure.Services
{
    internal class LinkBuilder : ILinkBuilder
    {


        private readonly IOptions<LinkOption> _options;


        public LinkBuilder(IOptions<LinkOption> options)
        {
            _options = options;
        }
        public string BuildPasswordResetLink(string email, string token)
        {

            var encodedToken = WebUtility.UrlEncode(token);
            return encodedToken;
        }
    }
}
