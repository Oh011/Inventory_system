using AutoMapper;
using Domain.Entities;
using MediatR;
using Project.Application.Common.Interfaces.Repositories;
using Project.Application.Features.Customers.Dtos;
using Project.Application.Features.Customers.Specifications;
using Shared.Results;

namespace Project.Application.Features.Customers.Queries.GetAllCustomers
{
    internal class GetAllCustomersQueryHandler : IRequestHandler<GetCustomersQuery, PaginatedResult<CustomerDto>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public GetAllCustomersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {


            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {

            var repository = _unitOfWork.GetRepository<Customer, int>();

            var specifications = new CustomerSpecifications(request);


            var customers = await repository.GetAllWithSpecifications(specifications);
            var totalCount = await repository.CountAsync(specifications.Criteria);


            var customerDtos = _mapper.Map<IEnumerable<CustomerDto>>(customers);

            return new PaginatedResult<CustomerDto>(
                request.PageIndex,
                request.pageSize,
                totalCount,
                customerDtos
            );
        }
    }
}
