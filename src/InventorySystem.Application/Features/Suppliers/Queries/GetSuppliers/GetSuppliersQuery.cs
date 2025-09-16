using MediatR;
using InventorySystem.Application.Common.Enums.SortOptions;
using InventorySystem.Application.Features.Suppliers.Dtos;
using Shared.Parameters;
using Shared.Results;

namespace InventorySystem.Application.Features.Suppliers.Queries.GetSuppliers
{
    public class GetSuppliersQuery : PaginationQueryParameters, IRequest<PaginatedResult<SupplierDto>>
    {

        public string? Name { get; set; }


        public int? CategoryId { get; set; }


        public DateTime? CreationDate { get; set; }

        public SupplierSortOptions? SortOptions { get; set; }

        public string? Email { get; set; }
        public string? ContactName { get; set; }
        public string? Phone { get; set; }
    }
}
