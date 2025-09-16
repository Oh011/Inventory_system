using MediatR;
using InventorySystem.Application.Features.Customers.Dtos;

namespace InventorySystem.Application.Features.Customers.Queries.CustomerLookup
{
    public class GetCustomerLookupQuery : IRequest<List<CustomerLookUpDto>>
    {
        public string Search { get; set; } = string.Empty;
    }

}
