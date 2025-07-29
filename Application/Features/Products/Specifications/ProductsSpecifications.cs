
using Domain.Entities;
using Domain.Specifications;
using Project.Application.Common.Enums.SortOptions;
using Project.Application.Features.Products.Dtos;
using Project.Application.Features.Products.queries.GetProducts;

namespace Application.Specifications.Products
{
    public class ProductsSpecifications : ProjectionSpecifications<Product, ProductResultDto>
    {



        public ProductsSpecifications(GetProductsQuery query) :

            base(p => (string.IsNullOrEmpty(query.Name) || p.Name.ToLower().Contains(query.Name.ToLower()))

            && (!query.CategoryId.HasValue || p.CategoryId == query.CategoryId.Value)

            && (!query.MinPrice.HasValue || p.SellingPrice >= query.MinPrice.Value)

            && (!query.MaxPrice.HasValue || p.SellingPrice <= query.MaxPrice.Value))

        {








            AddProjection(p => new ProductResultDto
            {
                Id = p.Id,
                Name = p.Name,

                Barcode = p.Barcode,
                Unit = p.Unit.ToString(), // assuming Unit is an enum
                SellingPrice = p.SellingPrice,

                ProductImageUrl = p.ProductImageUrl,
                CategoryId = p.CategoryId,
                CategoryName = p.Category != null ? p.Category.Name : null,
                IsCategoryDeleted = p.Category != null && p.Category.IsDeleted

            });


            switch (query.sortOptions)
            {
                case ProductSortOptions.NameAsc:
                    SetResultOrderBy(p => p.Name);
                    break;
                case ProductSortOptions.NameDesc:
                    SetResultOrderByDescending(p => p.Name);
                    break;

                case ProductSortOptions.SellingPriceAsc:
                    SetResultOrderBy(p => p.SellingPrice);
                    break;
                case ProductSortOptions.SellingPriceDec:
                    SetResultOrderByDescending(p => p.SellingPrice);
                    break;




                default:
                    SetResultOrderBy(p => p.Name); // fallback
                    break;
            }


            ApplyPagination(query.PageIndex, query.pageSize);

        }

    }
}
