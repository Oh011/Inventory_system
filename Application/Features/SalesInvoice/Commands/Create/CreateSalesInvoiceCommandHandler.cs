using Domain.Entities;
using MediatR;
using Project.Application.Common.Interfaces.Repositories;
using Project.Application.Common.Interfaces.Services;
using Project.Application.Common.Validators;
using Project.Application.Features.Inventory.Dtos;
using Project.Application.Features.SalesInvoice.Dtos;
using Project.Application.Features.SalesInvoice.Interfaces;

namespace Project.Application.Features.SalesInvoice.Commands.Create
{
    internal class CreateSalesInvoiceCommandHandler : IRequestHandler<CreateSalesInvoiceCommand, SalesInvoiceDetailsDto>
    {


        private readonly IUnitOfWork _unitOfWork;
        private readonly ISalesInvoiceService salesInvoiceService;
        private readonly IEntityValidator<Customer> _customerValidator;
        private readonly ICurrentUserService _currentUserService;
        private readonly IInventoryService _inventoryService;



        public CreateSalesInvoiceCommandHandler(IInventoryService inventoryService, IUnitOfWork unitOfWork, ISalesInvoiceService salesInvoiceService, IEntityValidator<Customer> validator, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _customerValidator = validator;
            this._inventoryService = inventoryService;
            this.salesInvoiceService = salesInvoiceService;
            _currentUserService = currentUserService;
        }
        public async Task<SalesInvoiceDetailsDto> Handle(CreateSalesInvoiceCommand request, CancellationToken cancellationToken)
        {


            var employeeRepository = _unitOfWork.GetRepository<Employee, int>();
            //var currentUserId = _currentUserService.UserId;
            //var employeeId = await employeeRepository.FirstOrDefaultAsync(e => e.ApplicationUserId == currentUserId,
            //    e => e.Id)
            //    ;






            await _customerValidator.ValidateExistsAsync(request.CustomerId, "Customer");


            var invoiceId = await salesInvoiceService.CreateSalesInvoice(request);




            var invoice = await salesInvoiceService.GetInvoiceById(invoiceId);

            var x = invoice.Items.Select(i => new InventoryStockAdjustmentDto
            {

                ProductId = i.ProductId,
                QuantityChange = -i.Quantity,
            }).ToList();

            await _inventoryService.AdjustStockAsync(x);

            return invoice;
        }
    }
}
