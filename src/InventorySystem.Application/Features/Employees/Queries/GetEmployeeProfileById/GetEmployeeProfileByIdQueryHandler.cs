using Domain.Entities;
using InventorySystem.Application.Common.Interfaces.Repositories;
using InventorySystem.Application.Common.Interfaces.Services.Interfaces;
using InventorySystem.Application.Features.Employees.Dtos;
using InventorySystem.Application.Features.Employees.Specifications;
using MediatR;
using Shared.Results;

namespace InventorySystem.Application.Features.Employees.Queries.GetEmployeeProfileById
{
    internal class GetEmployeeProfileByIdQueryHandler : IRequestHandler<GetEmployeeProfileByIdQuery, Result<EmployeeProfileDto>>
    {

        private readonly IUriService _uriService;
        private readonly IUnitOfWork unitOfWork;



        public GetEmployeeProfileByIdQueryHandler(IUriService uriService, IUnitOfWork unitOfWork)
        {

            this._uriService = uriService;
            this.unitOfWork = unitOfWork;
        }



        public async Task<Result<EmployeeProfileDto>> Handle(GetEmployeeProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var repository = unitOfWork.GetRepository<Employee, int>();
            var specifications = new EmployeeProfileSpecifications(null, request.employeeId);

            var result = await repository.GetByIdWithProjectionSpecifications(specifications);



            if (result == null)
                return Result<EmployeeProfileDto>.Failure($"Employee with Id :{request.employeeId} is not found", ErrorType.NotFound);


            if (result.ProfileImageUrl is not null)
                result.ProfileImageUrl = _uriService.GetAbsoluteUri(result.ProfileImageUrl);

            return Result<EmployeeProfileDto>.Success(result);
        }
    }
}
