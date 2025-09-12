using Domain.Entities;
using Domain.Specifications;
using Project.Application.Common.Enums.SortOptions;
using Project.Application.Features.Inventory.Dtos;
using Project.Application.Features.Inventory.Filters;

namespace Project.Application.Features.Inventory.Specifications
{
    public class GetAdjustmentLogsSpecifications : ProjectionSpecifications<StockAdjustmentLog, StockAdjustmentLogDto>
    {




        public GetAdjustmentLogsSpecifications(AdjustmentLogsFilter filter, int? pageIndex = null, int? pageSize = null)
         : base(a =>
             (!filter.ProductId.HasValue || a.ProductId == filter.ProductId) &&
             (!filter.AdjustedById.HasValue || a.AdjustedById == filter.AdjustedById) &&
             (!filter.FromDate.HasValue || a.AdjustedAt >= filter.FromDate.Value) &&
             (!filter.ToDate.HasValue || a.AdjustedAt <= filter.ToDate.Value) &&
             (!filter.MinChange.HasValue || a.QuantityChange >= filter.MinChange.Value) &&
             (!filter.MaxChange.HasValue || a.QuantityChange <= filter.MaxChange.Value)
         )
        {
            AddProjection(a => new StockAdjustmentLogDto
            {
                Id = a.Id,
                ProductId = a.ProductId,
                ProductName = a.Product.Name,
                QuantityChange = a.QuantityChange,
                Reason = a.Reason,
                AdjustedAt = a.AdjustedAt,
                AdjustedById = a.AdjustedById ?? 0,
                AdjustedByName = a.AdjustedBy.FullName
            });

            switch (filter.SortOptions)
            {
                case AdjustmentLogsSortOptions.DateAsc:
                    SetOrderBy(a => a.AdjustedAt);
                    break;
                case AdjustmentLogsSortOptions.DateDesc:
                    SetOrderByDescending(a => a.AdjustedAt);
                    break;
                case AdjustmentLogsSortOptions.QuantityAsc:
                    SetOrderBy(a => a.QuantityChange);
                    break;
                case AdjustmentLogsSortOptions.QuantityDesc:
                    SetOrderByDescending(a => a.QuantityChange);
                    break;
                default:
                    SetOrderByDescending(a => a.AdjustedAt);
                    break;
            }

            if (pageIndex.HasValue && pageSize.HasValue)
            {
                ApplyPagination(pageIndex.Value, pageSize.Value);
            }
        }

    }
}
