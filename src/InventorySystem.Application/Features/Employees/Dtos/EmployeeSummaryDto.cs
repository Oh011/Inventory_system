namespace InventorySystem.Application.Features.Employees.Dtos
{
    public class EmployeeSummaryDto
    {

        public int EmployeeId { get; set; }               // Application ID
        public string UserId { get; set; } = string.Empty; // Identity User ID

        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
        public string? JobTitle { get; set; }

        public bool IsActive { get; set; }
        public string? ProfileImageUrl { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
