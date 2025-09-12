namespace Project.Application.Features.Customers.Dtos
{
    public class CustomerLookUpDto
    {


        public int Id { get; set; }
        public string FullName { get; set; } = null!;

        public string? Phone { get; set; }
    }
}
