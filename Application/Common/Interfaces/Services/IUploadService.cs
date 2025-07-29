using Microsoft.AspNetCore.Http;

namespace Project.Application.Common.Interfaces.Services
{
    public interface IUploadService
    {

        bool Delete(string path);
        Task<string> Upload(IFormFile file, string FolderName);
    }
}
