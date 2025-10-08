using InventorySystem.Application.Common.Validators;
using InventorySystem.Application.Features.SalesInvoice.Dtos;
using InventorySystem.Application.Features.SalesInvoice.Interfaces;
using MediatR;
using salesInvoice = Domain.Entities.SalesInvoice;



namespace InventorySystem.Application.Features.SalesInvoice.Queries.GetInvoiceItems
{
    internal class GetInvoiceItemsQueryHandler : IRequestHandler<GetInvoiceItemsQuery, IEnumerable<SalesInvoiceItemDto>>
    {

        private readonly ISalesInvoiceService salesInvoiceService;
        private readonly IEntityValidator<salesInvoice> _validator;


        public GetInvoiceItemsQueryHandler(ISalesInvoiceService salesInvoiceService, IEntityValidator<salesInvoice> validator)
        {
            this._validator = validator;
            this.salesInvoiceService = salesInvoiceService;
        }
        public async Task<IEnumerable<SalesInvoiceItemDto>> Handle(GetInvoiceItemsQuery request, CancellationToken cancellationToken)
        {




            var items = await salesInvoiceService.GetInvoiceItems(request.Id);

            return items;
        }
    }
}
