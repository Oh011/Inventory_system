using Shared.Dtos;

namespace InventorySystem.Services
{
    // WebAPI/Services/FileUploadHelper.cs
    public static class FileUploadHelper
    {
        public static FileUploadDto? ToFileUploadDto(IFormFile? file)
        {
            if (file == null)
                return null;

            return new FileUploadDto
            {
                FileStream = file.OpenReadStream(),
                FileName = file.FileName,
                ContentType = file.ContentType,
                FileLength = file.Length,

            };
        }
    }

}
