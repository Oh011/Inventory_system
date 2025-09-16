using MediatR;
using InventorySystem.Application.Features.Products.Dtos;

namespace InventorySystem.Application.Features.Products.queries.ProductCreationData
{
    public class GetProductCreationDataQuery : IRequest<ProductCreationDataDto>
    {
    }
}
