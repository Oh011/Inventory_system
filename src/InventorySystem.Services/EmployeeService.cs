
using Application.Exceptions;
using Domain.Entities;
using Project.Application.Common.Interfaces.Repositories;
using Project.Application.Common.Interfaces.Services;
using Project.Application.Common.Parameters;
using Project.Application.Features.Employees.Commands.UpdateEmployee;
using Project.Application.Features.Employees.Commands.UpdateEmployeeImage;
using Project.Application.Features.Employees.Dtos;
using Project.Application.Features.Employees.Interfcaes;
using Project.Application.Features.Employees.Specifications;
using Shared.Results;

namespace Project.Services
{
    public class EmployeeService(IUnitOfWork unitOfWork, IUploadService uploadService, IAuthorizationService authorizationService) : IEmployeeService
    {
        public async Task<Employee> CreateEmployee(Employee employee)
        {

            var employeeRepository = unitOfWork.GetRepository<Employee, int>();




            try
            {

                await employeeRepository.AddAsync(employee);
                await unitOfWork.Commit();



            }
            catch (Exception ex)
            {
                unitOfWork.DetachTrackedEntity(employee);

                throw new Exception("Employee with this Id already exists.");
            }




            return employee;
        }

        public async Task<PaginatedResult<EmployeeSummaryDto>> GetAllEmployees(EmployeeFilterParams filters)
        {

            var repository = unitOfWork.GetRepository<Employee, int>();

            var specifications = new EmployeeSpecifications(filters);


            var employees = await repository.GetAllWithProjectionSpecifications(specifications);
            var totalCount = await repository.CountAsync(specifications.Criteria);


            return new PaginatedResult<EmployeeSummaryDto>(filters.PageIndex, filters.pageSize, totalCount
                , employees

                );
        }

        public async Task<Employee> UpdateEmployee(UpdateEmployeeCommand dto)
        {

            var repository = unitOfWork.GetRepository<Employee, int>();

            var employee = await repository.GetById(dto.Id);


            if (employee == null)
            {

                throw new NotFoundException("employee not found");
            }




            authorizationService.AuthorizeEmployeeAccess(employee.ApplicationUserId);






            if (!string.IsNullOrWhiteSpace(dto.FullName?.Trim()))
                employee.FullName = dto.FullName;

            if (!string.IsNullOrWhiteSpace(dto.JobTitle?.Trim()))
                employee.JobTitle = dto.JobTitle;

            if (!string.IsNullOrWhiteSpace(dto.Address?.Trim()))
                employee.Address = dto.Address;

            if (!string.IsNullOrWhiteSpace(dto.NationalId?.Trim()))
                employee.NationalId = dto.NationalId;

            repository.Update(employee);
            await unitOfWork.Commit();


            return employee;


        }

        public async Task<string?> UpdateEmployeeProfileImage(UpdateEmployeeProfileImageCommand request)
        {

            var repository = unitOfWork.GetRepository<Employee, int>();

            var employee = await repository.GetById(request.EmployeeId);


            if (employee == null)
            {

                throw new NotFoundException("employee not found");
            }


            authorizationService.EnsureSelf(employee.ApplicationUserId);

            var profileImagePath = "";


            if (employee.ProfileImageUrl != null)
            {

                uploadService.Delete(employee.ProfileImageUrl);

            }

            if (request.ProfileImage != null)
            {

                profileImagePath = await uploadService.Upload(request.ProfileImage, "Employees");

            }

            else
            {
                profileImagePath = null;
            }


            employee.ProfileImageUrl = profileImagePath;
            repository.Update(employee);

            await unitOfWork.Commit();


            return profileImagePath;


        }

        public async Task UpdateEmployeeRoleAsync(string userId, string newRole)
        {


            var repository = unitOfWork.GetRepository<Employee, int>();

            var employee = (await repository.FindAsync(

                e => e.ApplicationUserId == userId
                )).FirstOrDefault();



            if (employee == null)
                throw new NotFoundException("Employee not found");





            employee.Role = newRole;
            repository.Update(employee);
            await unitOfWork.Commit();


        }




    }
}
