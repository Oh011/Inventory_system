using MediatR;
using Project.Application.Features.Customers.Dtos;

namespace Project.Application.Features.Customers.Queries.CustomerLookup
{
    public class GetCustomerLookupQuery : IRequest<List<CustomerLookUpDto>>
    {
        public string Search { get; set; } = string.Empty;
    }

}
