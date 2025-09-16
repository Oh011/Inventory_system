using Domain.Entities;
using InventorySystem.Application.Common.Interfaces.Repositories;
using InventorySystem.Application.Common.Interfaces.Services.Interfaces;
using InventorySystem.Application.Common.Validators;
using InventorySystem.Application.Features.SalesInvoice.Dtos;
using InventorySystem.Application.Features.SalesInvoice.Interfaces;
using MediatR;

namespace InventorySystem.Application.Features.SalesInvoice.Commands.Create
{

    internal class ProductForSalesInvoiceDto
    {
        public int Id { get; set; }
        public decimal SellingPrice { get; set; }
    }
    internal class CreateSalesInvoiceCommandHandler : IRequestHandler<CreateSalesInvoiceCommand, SalesInvoiceDetailsDto>
    {


        private readonly IUnitOfWork _unitOfWork;
        private readonly ISalesInvoiceService salesInvoiceService;
        private readonly IEntityValidator<Customer> _customerValidator;
        private readonly IEntityValidator<Product> _productsValidator;

        private readonly ICurrentUserService _currentUserService;





        public CreateSalesInvoiceCommandHandler(IEntityValidator<Product> productsValidator, IInventoryService inventoryService, IUnitOfWork unitOfWork, ISalesInvoiceService salesInvoiceService, IEntityValidator<Customer> validator, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;

            this._productsValidator = productsValidator;


            _customerValidator = validator;

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
            return invoice;
        }
    }
}
