
using Domain.Entities;
using MediatR;
using InventorySystem.Application.Common.Interfaces.Repositories;
using InventorySystem.Application.Features.Suppliers.Dtos;

namespace InventorySystem.Application.Features.Suppliers.Queries.SupplierLookUp
{
    internal class SupplierLookUpQueryHandler : IRequestHandler<SupplierLookUpQuery, IEnumerable<SupplierLookupDto>>
    {

        private readonly IUnitOfWork unitOfWork;



        public SupplierLookUpQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<SupplierLookupDto>> Handle(SupplierLookUpQuery request, CancellationToken cancellationToken)
        {
            var repository = unitOfWork.GetRepository<Supplier, int>();
            var result = await repository.ListAsync(s => new SupplierLookupDto
            {
                Id = s.Id,
                Name = s.Name,
            });



            return result;
        }
    }
}
