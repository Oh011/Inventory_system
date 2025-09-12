using Application.Exceptions;
using Domain.Entities;
using Project.Application.Common.Interfaces.Repositories;
using Project.Application.Common.Interfaces.Services;

namespace Infrastructure.Services
{
    internal class EmployeeContextService : IEmployeeContextService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeContextService(
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> GetCurrentEmployeeIdAsync()
        {
            var employeeRepository = _unitOfWork.GetRepository<Employee, int>();

            var userId = _currentUserService.UserId;

            if (string.IsNullOrWhiteSpace(userId))
                throw new UnauthorizedAccessException("User is not authenticated.");

            var employeeId = await employeeRepository.FirstOrDefaultAsync(
                e => e.ApplicationUserId == userId,
                e => e.Id);

            if (employeeId == default)
                throw new NotFoundException("Employee profile not found for current user.");

            return employeeId;
        }
    }
}
