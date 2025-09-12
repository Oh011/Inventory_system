using MediatR;
using Project.Application.Common.Enums.SortOptions;
using Project.Application.Features.Customers.Dtos;
using Shared.Parameters;
using Shared.Results;

namespace Project.Application.Features.Customers.Queries.GetAllCustomers
{
    public class GetCustomersQuery : PaginationQueryParameters, IRequest<PaginatedResult<CustomerDto>>
    {


        public string? Name { get; set; }          // Search by full name (contains)
        public string? Phone { get; set; }         // Filter by exact or partial phone
        public string? Email { get; set; }         // Filter by exact or partial email
        public string? Address { get; set; }       // Optional, filter by address (contains)



        public CustomerSortOptions CustomerSortOptions { get; set; }
    }
}
