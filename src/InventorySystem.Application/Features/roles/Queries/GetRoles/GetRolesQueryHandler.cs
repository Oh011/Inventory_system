using Application.Features.roles.Dtos;
using Application.Features.roles.Interfaces;
using Domain.Entities;
using MediatR;
using InventorySystem.Application.Common.Interfaces.Repositories;

namespace Application.Features.roles.Queries.GetRoles
{
    internal class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<RoleDto>>
    {


        private readonly IRoleService roleService;
        private readonly IUnitOfWork unitOfWork;
        private ITransactionManager transactionManager;

        public GetRolesQueryHandler(IRoleService roleService, IUnitOfWork unitOfWork, ITransactionManager transactionManager)
        {

            this.unitOfWork = unitOfWork;
            this.transactionManager = transactionManager;
            this.roleService = roleService;
        }
        public async Task<List<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {

            var repository = unitOfWork.GetRepository<Product, int>();

            this.transactionManager = await unitOfWork.BeginTransaction(cancellationToken);

            var product = await repository.GetById(2);
            product.QuantityInStock = product.QuantityInStock - 3;

            await unitOfWork.Commit();

            await transactionManager.CommitTransaction(cancellationToken);


            return await roleService.GetAllRolesAsync();

        }
    }
}
