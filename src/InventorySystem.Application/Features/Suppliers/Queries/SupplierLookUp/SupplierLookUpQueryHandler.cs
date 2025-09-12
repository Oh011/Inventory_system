
using Domain.Entities;
using MediatR;
using Project.Application.Common.Interfaces.Repositories;
using Project.Application.Features.Suppliers.Dtos;

namespace Project.Application.Features.Suppliers.Queries.SupplierLookUp
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
