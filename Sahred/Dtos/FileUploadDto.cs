namespace Shared.Dtos
{
    // Shared/DTOs/FileUploadDto.cs
    public class FileUploadDto
    {
        public Stream FileStream { get; set; } = default!;
        public string FileName { get; set; } = default!;

        public long FileLength { get; set; }
        public string ContentType { get; set; } = default!;
    }

}
