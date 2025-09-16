using InventorySystem.Application.Common.Enums.SortOptions;
using InventorySystem.Application.Features.Customers.Dtos;
using MediatR;
using Shared.Parameters;
using Shared.Results;

namespace InventorySystem.Application.Features.Customers.Queries.GetAllCustomers
{
    public class GetCustomersQuery : PaginationQueryParameters, IRequest<PaginatedResult<CustomerDto>>
    {


        public string? Name { get; set; }          // Search by full name (contains)
        public string? Phone { get; set; }         // Filter by exact or partial phone
        public string? Email { get; set; }         // Filter by exact or partial email
        public string? Address { get; set; }       // Optional, filter by address (contains)

        public bool? IsDeleted { get; set; } = null;

        public CustomerSortOptions CustomerSortOptions { get; set; }
    }
}
