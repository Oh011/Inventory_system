using Domain.Entities;
using Domain.Specifications;
using Project.Application.Features.Inventory.Dtos;
using Project.Application.Features.Inventory.Queries.GetInventoryValue;

namespace Project.Application.Features.Inventory.Specifications
{
    internal class InventoryValueReportSpecifications : ProjectionSpecifications<Product, InventoryValueDto>
    {




        public InventoryValueReportSpecifications(GetInventoryValueReportQuery query)
    : base(p =>
        (string.IsNullOrWhiteSpace(query.Name) || p.Name.ToLower().Contains(query.Name.ToLower())) &&
        (!query.CategoryId.HasValue || p.CategoryId == query.CategoryId.Value)
    )
        {
            AddInclude(p => p.Category);

            AddProjection(p => new InventoryValueDto
            {
                ProductId = p.Id,
                ProductName = p.Name,
                Unit = p.Unit.ToString(),
                CategoryName = p.Category != null ? p.Category.Name : null,
                CostPrice = p.CostPrice,
                QuantityInStock = p.QuantityInStock
            });
        }

    }
}
