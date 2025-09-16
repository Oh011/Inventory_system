namespace InventorySystem.Application.Features.Employees.Dtos
{
    public class EmployeeBaseDto
    {

        public string FullName { get; set; } = string.Empty;
        public string? JobTitle { get; set; }
        public string? Address { get; set; }

        public string? Role { get; set; }
        public string? NationalId { get; set; }
    }
}
