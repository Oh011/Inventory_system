using Application.Exceptions;
using Domain.Entities;
using Domain.ValueObjects.SalesInvoice.Domain.ValueObjects;
using InventorySystem.Application.Common.Interfaces.Repositories;
using InventorySystem.Application.Common.Interfaces.Services.Interfaces;
using InventorySystem.Application.Common.Validators;
using InventorySystem.Application.Features.Inventory.Dtos;
using InventorySystem.Application.Features.SalesInvoice.Commands.Create;
using InventorySystem.Application.Features.SalesInvoice.Dtos;
using InventorySystem.Application.Features.SalesInvoice.Interfaces;
using InventorySystem.Application.Features.SalesInvoice.Queries.GetAll;
using InventorySystem.Application.Features.SalesInvoice.specifications;
using Shared.Errors;
using Shared.Results;

namespace InventorySystem.Services
{


    internal class ProductForSalesInvoiceDto
    {
        public int Id { get; set; }

        public decimal SellingPrice { get; set; }
    }
    internal class SalesInvoiceService(IStockEventService stockEventService, ITransactionManager transactionManager, IInventoryService _inventoryService, IUnitOfWork unitOfWork, IEntityValidator<SalesInvoice> _validator) : ISalesInvoiceService
    {



        public async Task<int> CreateSalesInvoice(CreateSalesInvoiceCommand Invoice)
        {


            var SalesInvoiceRepository = unitOfWork.GetRepository<SalesInvoice, int>();

            var productRepository = unitOfWork.GetRepository<Product, int>();

            var InvoiceProductsIds = Invoice.Items.Select(i => i.ProductId).ToList();

            var products = (await productRepository.ListAsync(p => InvoiceProductsIds.Contains(p.Id),

                p => new ProductForSalesInvoiceDto
                {
                    Id = p.Id,
                    SellingPrice = p.SellingPrice,
                })).ToDictionary(p => p.Id, p => p.SellingPrice);




            var missingIds = InvoiceProductsIds.Except(products.Select(i => i.Key).ToList());


            if (missingIds.Any())
            {

                var errors = missingIds.ToDictionary(
                   id => id.ToString(),
                   id => new List<ValidationErrorDetail>
                   {
                        new ValidationErrorDetail($"Product with ID {id} not found")
                   }
                    );

                throw new ValidationException(errors, $"Some Products are invalid");
            }


            foreach (var item in Invoice.Items)
            {
                item.SellingPrice = products[item.ProductId];

            }



            var itemDataList = Invoice.Items.Select(i => new SalesInvoiceItemData(
               i.ProductId,
               i.QuantitySold,
               i.SellingPrice,
               i.Discount ?? 0m
           )).ToList();


            var invoice = new SalesInvoice
            {

                CustomerId = Invoice.CustomerId,
                CreatedByEmployeeId = Invoice.CreatedByEmployeeId,
                InvoiceDate = Invoice.InvoiceDate,
                Notes = Invoice.Notes,
                InvoiceDiscount = Invoice.DiscountAmount,
                DeliveryFee = 0,
                PaymentMethod = Invoice.PaymentMethod,

            };


            invoice.AddItems(itemDataList);



            var adjustItems = Invoice.Items.Select(
             i => new InventoryStockAdjustmentDto
             {

                 ProductId = i.ProductId,
                 QuantityChange = -i.QuantitySold
             }
             ).ToList();



            await transactionManager.BeginTransactionAsync();



            try
            {
                await SalesInvoiceRepository.AddAsync(invoice);

                var productIds = await _inventoryService.AdjustStockAsync(adjustItems, transactionManager);
                await unitOfWork.Commit();
                await transactionManager.CommitTransaction();
                await stockEventService.RaiseStockDecreasedEventAsync(productIds);

                return invoice.Id;

            }

            catch (Exception ex)
            {

                await transactionManager.RollBackTransaction();
                throw;
            }




        }

        public async Task<PaginatedResult<SalesInvoiceSummaryDto>> GetAllInvoices(GetSalesInvoicesQuery query)
        {

            var repository = unitOfWork.GetRepository<SalesInvoice, int>();
            var specifications = new SalesInvoiceSpecifications(query);

            var invoices = await repository.GetAllWithProjectionSpecifications(specifications);
            var totalCount = await repository.CountAsync(specifications.Criteria);


            return new PaginatedResult<SalesInvoiceSummaryDto>(

                query.PageIndex, query.pageSize, totalCount, invoices

                );
        }

        public async Task<SalesInvoiceDetailsDto> GetInvoiceById(int id)
        {

            var repository = unitOfWork.GetRepository<SalesInvoice, int>();

            var specifications = new SalesInvoiceDetailsSpecifications(id);

            var invoice = await repository.GetByIdWithProjectionSpecifications(specifications);


            if (invoice == null)
            {
                throw new NotFoundException($"Invoice with Id :{id} is not found");
            }


            return invoice;
        }

        public async Task<IEnumerable<SalesInvoiceItemDto>> GetInvoiceItems(int id)
        {

            await _validator.ValidateExistsAsync(id, "Invoice");
            var repository = unitOfWork.GetRepository<SalesInvoiceItem, int>();
            var items = await repository.ListAsync(i => i.SalesInvoiceId == id,
                item => new SalesInvoiceItemDto
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product != null ? item.Product.Name : "Unknown",
                    QuantitySold = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Discount = item.Discount
                });



            return items;


        }


        public async Task<IEnumerable<SalesInvoiceItemWithReturnInfoDto>> GetInvoiceItemsWithReturnInfo(int id)
        {


            await _validator.ValidateExistsAsync(id, "Invoice");
            var repository = unitOfWork.GetRepository<SalesInvoiceItem, int>();
            var items = await repository.ListAsync(i => i.SalesInvoiceId == id, item => new SalesInvoiceItemWithReturnInfoDto
            {
                ProductId = item.ProductId,
                ProductName = item.Product != null ? item.Product.Name : "Unknown",
                QuantitySold = item.Quantity,
                UnitPrice = item.UnitPrice,
                Discount = item.Discount,
                ReturnedQuantity = item.SalesReturnItems.Sum(r => r.Quantity)

            });


            return items;



        }
    }
}
