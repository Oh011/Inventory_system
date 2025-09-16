using Domain.Entities;
using Domain.Specifications;
using InventorySystem.Application.Common.Enums.SortOptions;
using InventorySystem.Application.Features.Products.Dtos;
using InventorySystem.Application.Features.Products.queries.GetProductsForSupplier;

namespace InventorySystem.Application.Features.Products.Specifications
{
    internal class SupplierProductsSpecifications : ProjectionSpecifications<Product, ProductPurchaseOrderLookUpDto>
    {


        public SupplierProductsSpecifications(GetPurchaseProductsLookupQuery filter) :

            base(p => (p.Category.SupplierCategories.Any(sc => sc.SupplierId == filter.SupplierId) && !p.IsDeleted) &&
             (string.IsNullOrEmpty(filter.Name) || p.Name.ToLower().Contains(filter.Name.ToLower()))

            && (!filter.CategoryId.HasValue || p.CategoryId == filter.CategoryId.Value)

            )

        {


            AddInclude(p => p.Category);



            AddProjection(p => new ProductPurchaseOrderLookUpDto
            {
                Id = p.Id,
                Name = p.Name,
                Barcode = p.Barcode,
                Unit = p.Unit.ToString(), // assuming Unit is an enum    
                QuantityInStock = p.QuantityInStock,
                CategoryId = p.CategoryId,
                CostPrice = p.CostPrice,


            });


            switch (filter.SortOptions)
            {
                case ProductSortOptions.NameAsc:
                    SetResultOrderBy(p => p.Name);
                    break;
                case ProductSortOptions.NameDesc:
                    SetResultOrderByDescending(p => p.Name);
                    break;




                case ProductSortOptions.QuantityInStockAsc:
                    SetResultOrderBy(p => p.QuantityInStock);
                    break;
                case ProductSortOptions.QuantityInStockDesc:
                    SetResultOrderByDescending(p => p.QuantityInStock);
                    break;

                default:
                    SetResultOrderBy(p => p.Name); // fallback
                    break;
            }


            ApplyPagination(filter.PageIndex, filter.pageSize);

        }
    }
}
