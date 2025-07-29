using MediatR;
using Project.Application.Features.Products.Dtos;

namespace Project.Application.Features.Products.queries.ProductCreationData
{
    public class GetProductCreationDataQuery : IRequest<ProductCreationDataDto>
    {
    }
}
