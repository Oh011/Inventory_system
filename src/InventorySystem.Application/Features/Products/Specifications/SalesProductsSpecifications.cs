using Domain.Entities;
using Domain.Specifications;
using InventorySystem.Application.Common.Enums.SortOptions;
using InventorySystem.Application.Features.Products.Dtos;
using InventorySystem.Application.Features.Products.queries.GetSalesProducts;

namespace InventorySystem.Application.Features.Products.Specifications
{
    internal class SalesProductsSpecifications : ProjectionSpecifications<Product, ProductSalesLookupDto>
    {
        public SalesProductsSpecifications(GetSalesProductsLookupQuery query)
            : base(p =>
                !p.IsDeleted &&
                (string.IsNullOrEmpty(query.Name) || p.Name.ToLower().Contains(query.Name.ToLower())) &&
                (!query.CategoryId.HasValue || p.CategoryId == query.CategoryId.Value)
            )
        {
            AddOrderBy(query.SortOptions);


            AddProjection(p => new ProductSalesLookupDto
            {
                Id = p.Id,
                Name = p.Name,
                Barcode = p.Barcode,
                Unit = p.Unit.ToString(),
                SellingPrice = p.SellingPrice,
                ProductImageUrl = p.ProductImageUrl,
                QuantityInStock = p.QuantityInStock,
                CategoryId = p.CategoryId
            });


            ApplyPagination(query.PageIndex, query.pageSize);
        }

        private void AddOrderBy(ProductSortOptions? sortOptions)
        {
            switch (sortOptions)
            {
                case ProductSortOptions.NameAsc:
                    SetOrderBy(p => p.Name);
                    break;
                case ProductSortOptions.NameDesc:
                    SetOrderByDescending(p => p.Name);
                    break;
                case ProductSortOptions.SellingPriceAsc:
                    SetOrderBy(p => p.SellingPrice);
                    break;
                case ProductSortOptions.SellingPriceDec:
                    SetOrderByDescending(p => p.SellingPrice);
                    break;
                default:
                    SetOrderBy(p => p.Name);
                    break;
            }
        }
    }

}
