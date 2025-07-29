using Domain.Entities;
using Domain.Specifications;
using Project.Application.Common.Enums.SortOptions;
using Project.Application.Features.Products.Dtos;
using Project.Application.Features.Products.queries.GetSalesProducts;

namespace Project.Application.Features.Products.Specifications
{
    internal class SalesProductsSpecifications : ProjectionSpecifications<Product, SalesProductDto>
    {
        public SalesProductsSpecifications(GetSalesProductsLookupQuery query)
            : base(p =>
                !p.IsDeleted &&
                (string.IsNullOrEmpty(query.Name) || p.Name.ToLower().Contains(query.Name.ToLower())) &&
                (!query.CategoryId.HasValue || p.CategoryId == query.CategoryId.Value)
            )
        {
            AddOrderBy(query.SortOptions);


            AddProjection(p => new SalesProductDto
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
