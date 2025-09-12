namespace Project.Application.Features.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeRequest
    {



        public string? FullName { get; set; }
        public string? JobTitle { get; set; }
        public string? Address { get; set; }
        public string? NationalId { get; set; }
    }
}
