namespace Project.Application.Features.Inventory.Queries.GetInventoryValue
{
    using MediatR;
    using Project.Application.Features.Inventory.Dtos;

    public class GetInventoryValueReportQuery : IRequest<InventoryValueReportResultDto>
    {
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
    }

}
