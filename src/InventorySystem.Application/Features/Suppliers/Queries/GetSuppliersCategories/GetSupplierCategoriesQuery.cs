using MediatR;
using Project.Application.Features.Categories.Dtos;

namespace Project.Application.Features.Suppliers.Queries.GetSuppliersCategories
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
