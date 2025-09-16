using AutoMapper;
using Domain.Entities;
using InventorySystem.Application.Common.Interfaces.Repositories;
using InventorySystem.Application.Features.Customers.Dtos;
using MediatR;

namespace InventorySystem.Application.Features.Customers.Queries.CustomerLookup
{
    public class GetCustomerLookupQueryHandler : IRequestHandler<GetCustomerLookupQuery, List<CustomerLookUpDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCustomerLookupQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CustomerLookUpDto>> Handle(GetCustomerLookupQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Customer, int>();

            if (string.IsNullOrWhiteSpace(request.Search))
                return new List<CustomerLookUpDto>();


            var customers = await repo.ListAsync(
                c => c.IsDeleted == false &&
                    (string.IsNullOrEmpty(request.Search) ||
                    c.FullName.Contains(request.Search) ||
                    (c.Phone != null && c.Phone.Contains(request.Search))), c => new CustomerLookUpDto
                    {

                        Id = c.Id,
                        FullName = c.FullName,
                        Phone = c.Phone,
                    }

            );

            return customers;
        }
    }

}
