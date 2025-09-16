using Shared.Dtos;

namespace InventorySystem.Application.Common.Interfaces.Services.Interfaces
{
    public interface IUploadService
    {

        bool Delete(string path);
        Task<string> Upload(FileUploadDto file, string FolderName);
    }
}
