namespace InventorySystem.Application.Features.SalesReturn.Commands.Create
{
    //internal class CreateSalesReturnCommandHandler : IRequestHandler<CreateSalesReturnCommand, Result<SalesReturnDto>>
    //{

    //    private readonly IUnitOfWork unitOfWork;
    //    private readonly ISalesInvoiceService salesInvoiceService;


    //    public CreateSalesReturnCommandHandler(IUnitOfWork unitOfWork,ISalesInvoiceService salesInvoiceService)
    //    {
    //        this.unitOfWork = unitOfWork;
    //        this.salesInvoiceService = salesInvoiceService;
    //    }

    //    public async Task<Result<SalesReturnDto>> Handle(CreateSalesReturnCommand request, CancellationToken cancellationToken)
    //    {

    //        var repository = unitOfWork.GetRepository<salesInvoice, int>();

    //        var invoice = await salesInvoiceService.GetInvoiceById(request.SalesInvoiceId);

    //        if (invoice == null)
    //            return Result<SalesReturnDto>.Failure("Invoice not found", ErrorType.NotFound);



    //    }
    //}
}
