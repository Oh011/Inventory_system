using AutoMapper;
using MediatR;
using InventorySystem.Application.Features.Suppliers.Dtos;
using InventorySystem.Application.Features.Suppliers.Interfaces;
using Shared.Results;

namespace InventorySystem.Application.Features.Suppliers.Queries.GetSuppliers
{
    internal class GetSuppliersQueryHandler : IRequestHandler<GetSuppliersQuery, PaginatedResult<SupplierDto>>
    {

        private readonly ISupplierService supplierService;
        private readonly IMapper mapper;


        public GetSuppliersQueryHandler(ISupplierService supplierService, IMapper mapper)
        {

            this.mapper = mapper;

            this.supplierService = supplierService;
        }
        public async Task<PaginatedResult<SupplierDto>> Handle(GetSuppliersQuery request, CancellationToken cancellationToken)
        {


            var result = await supplierService.GetAllSuppliers(request);
            var mappedSuppliers = mapper.Map<IEnumerable<SupplierDto>>(result.Items);



            var paginatedResult = new PaginatedResult<SupplierDto>(

                result.PageIndex,
                result.PageSize,
                result.TotalCount,
                mappedSuppliers
            );

            return paginatedResult;
        }
    }
}
