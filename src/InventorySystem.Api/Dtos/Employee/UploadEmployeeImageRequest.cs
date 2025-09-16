namespace InventorySystem.Api.Dtos.Employee
{
    public class UploadEmployeeImageRequest
    {

        public IFormFile ImageFile { get; set; } = default!;
    }

}
