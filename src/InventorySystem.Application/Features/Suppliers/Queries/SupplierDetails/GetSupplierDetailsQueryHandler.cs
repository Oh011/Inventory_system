using MediatR;
using Project.Application.Features.Suppliers.Dtos;
using Project.Application.Features.Suppliers.Interfaces;

namespace Project.Application.Features.Suppliers.Queries.SupplierDetails
{
    internal class GetSupplierDetailsQueryHandler : IRequestHandler<GetSupplierDetailsQuery, SupplierDetailDto>
    {


        private ISupplierService _supplierService;


        public GetSupplierDetailsQueryHandler(ISupplierService supplierService)
        {

            _supplierService = supplierService;

        }
        public Task<SupplierDetailDto> Handle(GetSupplierDetailsQuery request, CancellationToken cancellationToken)
        {


            var result = _supplierService.GetSupplierById(request.Id);


            return result;
        }
    }
}
