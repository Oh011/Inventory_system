using Domain.Entities;
using InventorySystem.Application.Common.Interfaces.Repositories;
using InventorySystem.Application.Common.Interfaces.Services.Interfaces;
using InventorySystem.Application.Features.Employees.Dtos;
using InventorySystem.Application.Features.Employees.Specifications;
using MediatR;
using Shared.Results;

namespace InventorySystem.Application.Features.Employees.Queries.GetEmployeeProfile
{
    internal class GetMyEmployeeProfileQueryHandler : IRequestHandler<GetEmployeeProfileQuery, Result<EmployeeProfileDto>>
    {

        private readonly IUriService _uriService;
        private readonly IUnitOfWork unitOfWork;


        public GetMyEmployeeProfileQueryHandler(IUriService uriService, IUnitOfWork unitOfWork)
        {

            _uriService = uriService;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<EmployeeProfileDto>> Handle(GetEmployeeProfileQuery request, CancellationToken cancellationToken)
        {


            var repository = unitOfWork.GetRepository<Employee, int>();
            var specifications = new EmployeeProfileSpecifications(request.userId, null);

            var result = await repository.GetByIdWithProjectionSpecifications(specifications);



            if (result.ProfileImageUrl is not null)
                result.ProfileImageUrl = _uriService.GetAbsoluteUri(result.ProfileImageUrl);


            return Result<EmployeeProfileDto>.Success(result);

        }
    }
}
