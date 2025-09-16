using InventorySystem.Application.Common.Interfaces.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    internal class UriService : IUriService
    {


        private readonly IHttpContextAccessor _contextAccessor;


        public UriService(IHttpContextAccessor contextAccessor)
        {
            this._contextAccessor = contextAccessor;
        }


        public string GetAbsoluteUri(string relativePath)
        {

            if (string.IsNullOrEmpty(relativePath))
                return null;


            var request = _contextAccessor.HttpContext?.Request;
            if (request == null)
                return relativePath;

            var baseUrl = $"{request.Scheme}://{request.Host.Value}";

            return $"{baseUrl}/{relativePath}".Replace("\\", "/");
        }
    }
}
