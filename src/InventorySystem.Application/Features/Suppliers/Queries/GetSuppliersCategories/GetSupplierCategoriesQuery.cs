using MediatR;
using InventorySystem.Application.Features.Categories.Dtos;

namespace InventorySystem.Application.Features.Suppliers.Queries.GetSuppliersCategories
{
    public class GetSupplierCategoriesQuery : IRequest<IEnumerable<CategoryDto>>
    {
        public int SupplierId { get; set; }

        public GetSupplierCategoriesQuery(int supplierId)
        {
            SupplierId = supplierId;
        }
    }

}
