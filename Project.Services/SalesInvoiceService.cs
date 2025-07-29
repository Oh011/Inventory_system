using Application.Exceptions;
using Domain.Entities;
using Domain.Exceptions;
using Domain.ValueObjects.SalesInvoice.Domain.ValueObjects;
using Project.Application.Common.Interfaces;
using Project.Application.Common.Interfaces.Repositories;
using Project.Application.Features.SalesInvoice.Commands.Create;
using Project.Application.Features.SalesInvoice.Dtos;
using Project.Application.Features.SalesInvoice.Interfaces;
using Project.Application.Features.SalesInvoice.Queries.GetAll;
using Project.Application.Features.SalesInvoice.specifications;
using Shared.Results;

namespace Project.Services
{


    internal class ProductForSalesInvoiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Barcode { get; set; } = null!;


        public int Stock { get; set; }
        public decimal SellingPrice { get; set; }
    }
    internal class SalesInvoiceService(IUnitOfWork unitOfWork, IDomainEventDispatcher eventDispatcher) : ISalesInvoiceService
    {



        public async Task<int> CreateSalesInvoice(CreateSalesInvoiceCommand Invoice)
        {


            var SalesInvoiceRepository = unitOfWork.GetRepository<SalesInvoice, int>();

            var productsIds = Invoice.Items.Select(order => order.ProductId).ToList();
            var products = await LoadProducts(productsIds);


            if (products.Count < productsIds.Count)
            {
                throw new NotFoundException("One of the products in the order is not found");
            }




            foreach (var item in Invoice.Items)
            {


                var product = products[item.ProductId];

                if (product.Stock < item.QuantitySold)
                {
                    throw new DomainException($"Insufficient stock for product {product.Name} (ID: {product.Id})");
                }

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


            await SalesInvoiceRepository.AddAsync(invoice);



            return invoice.Id;

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

        private async Task<Dictionary<int, ProductForSalesInvoiceDto>> LoadProducts(IEnumerable<int> productIds)
        {


            //→ Keep LoadProducts(...) inside PurchaseOrderService for now.
            //If other modules start using the same logic, refactor it later into ProductService.

            var productsRepository = unitOfWork.GetRepository<Product, int>();
            var products = (await productsRepository.ListAsync(
                        p => productIds.Contains(p.Id),
                        p => new ProductForSalesInvoiceDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Barcode = p.Barcode,
                            Stock = p.QuantityInStock,
                            SellingPrice = p.SellingPrice,
                        }
                    )).ToDictionary(p => p.Id, p => p);

            return products;

        }
    }
}
