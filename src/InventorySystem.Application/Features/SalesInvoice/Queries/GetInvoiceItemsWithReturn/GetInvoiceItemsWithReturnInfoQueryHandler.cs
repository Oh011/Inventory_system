using InventorySystem.Application.Common.Validators;
using InventorySystem.Application.Features.SalesInvoice.Dtos;
using InventorySystem.Application.Features.SalesInvoice.Interfaces;
using MediatR;
using Shared.Results;
using salesInvoice = Domain.Entities.SalesInvoice;


namespace InventorySystem.Application.Features.SalesInvoice.Queries.GetInvoiceItemsWithReturn
{
    internal class GetInvoiceItemsWithReturnInfoQueryHandler : IRequestHandler<GetInvoiceItemsWithReturnInfoQuery, Result<IEnumerable<SalesInvoiceItemWithReturnInfoDto>>>
    {


        private readonly ISalesInvoiceService salesInvoiceService;
        private readonly IEntityValidator<salesInvoice> _validator;


        public GetInvoiceItemsWithReturnInfoQueryHandler(ISalesInvoiceService salesInvoiceService, IEntityValidator<salesInvoice> validator)
        {
            this._validator = validator;
            this.salesInvoiceService = salesInvoiceService;
        }

        public async Task<Result<IEnumerable<SalesInvoiceItemWithReturnInfoDto>>> Handle(GetInvoiceItemsWithReturnInfoQuery request, CancellationToken cancellationToken)
        {


            var items = await salesInvoiceService.GetInvoiceItemsWithReturnInfo(request.id);

            return Result<IEnumerable<SalesInvoiceItemWithReturnInfoDto>>.Success(items);
        }
    }
}
