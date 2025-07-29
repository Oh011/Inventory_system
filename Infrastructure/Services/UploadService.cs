using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Project.Application.Common.Interfaces.Services;

namespace Infrastructure.Services
{
    internal class UploadService : IUploadService
    {



        private readonly List<string> _AllowedExtensions = new List<string>()
        {


            ".png",".jpg",".jpeg"
        };




        private int _MaxSize = 2_097_152; //--> 2MB {written in bytes}


        public bool Delete(string path)
        {

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", path.Replace("/", "\\"));
            if (File.Exists(fullPath))
            {

                File.Delete(fullPath);
                return true;
            }

            return false;
        }

        public async Task<string> Upload(IFormFile file, string FolderName)
        {


            var extension = Path.GetExtension(file.FileName);


            if (!_AllowedExtensions.Contains(extension))
            {
                var fileException = new ValidationException();
                fileException.AddValidations("ProfileImage", "Only .png, .jpg, .jpeg files are allowed.");
            }




            if (file.Length > _MaxSize)
            {
                var sizeException = new ValidationException();
                sizeException.AddValidations("ProfileImage", "ProfileImage size exceeds 2MB limit.");
            }


            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads", FolderName);


            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var FileName = $"{Guid.NewGuid()}{extension}";


            var FilePath = Path.Combine(folderPath, FileName);


            using var FileStream = new FileStream(FilePath, FileMode.Create);

            await file.CopyToAsync(FileStream);


            return Path.Combine("uploads", FolderName, FileName).Replace("\\", "/");

        }
    }
}
