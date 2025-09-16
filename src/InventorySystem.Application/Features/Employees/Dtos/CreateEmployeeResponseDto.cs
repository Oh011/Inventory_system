namespace InventorySystem.Application.Features.Employees.Dtos
{
    public class CreateEmployeeResponseDto : EmployeeBaseDto
    {
        public string UserId { get; set; } // Identity User Id
        public int EmployeeId { get; set; } // Application Employee Id
        public string Email { get; set; }




    }
}
