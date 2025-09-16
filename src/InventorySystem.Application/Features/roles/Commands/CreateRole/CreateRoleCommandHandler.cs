using Application.Features.roles.Dtos;
using Application.Features.roles.Interfaces;
using MediatR;
using InventorySystem.Application.Common.Interfaces.Repositories;

namespace Application.Features.roles.Commands.CreateRole
{
    internal class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, RoleDto>
    {

        private readonly IRoleService roleService;
        private readonly IUnitOfWork unitOfWork;
        private ITransactionManager transactionManager;

        public CreateRoleCommandHandler(IRoleService roleService, ITransactionManager transactionManager)
        {

            this.roleService = roleService;

        }
        public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {


            return await roleService.CreateRoleAsync(request.RoleName);
        }
    }
}
