namespace InventorySystem.Application.Features.Employees.Dtos
{
    public class EmployeeProfileDto
    {
        public int EmployeeId { get; set; }              // Business entity ID
        public string UserId { get; set; } = string.Empty; // Identity User ID

        // Identity-related info
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        // Business-related info
        public string? JobTitle { get; set; }
        public string? Address { get; set; }
        public string? NationalId { get; set; }
        public bool IsActive { get; set; }

        // Profile
        public string? ProfileImageUrl { get; set; }

        // Audit
        public DateTime CreatedAt { get; set; }
    }

}
