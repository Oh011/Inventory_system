using Shared.Dtos;

namespace Project.Application.Common.Interfaces.Services
{
    public interface IUploadService
    {

        bool Delete(string path);
        Task<string> Upload(FileUploadDto file, string FolderName);
    }
}
