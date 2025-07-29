using Domain.Entities;
using MediatR;
using Project.Application.Common.Interfaces.Repositories;
using Project.Application.Common.Interfaces.Services;
using Project.Application.Features.Inventory.Dtos;

namespace Project.Application.Features.Inventory.Commands.AdjustInventory
{
    internal class AdjustInventoryCommandHandler : IRequestHandler<AdjustInventoryCommand, string>
    {


        private readonly IUnitOfWork unitOfWork;
        private readonly IEmployeeContextService _employeeContext;
        private readonly IInventoryService _InventoryService;






        public AdjustInventoryCommandHandler(IInventoryService inventoryService, IUnitOfWork unitOfWork, IEmployeeContextService employeeContextService)
        {
            this.unitOfWork = unitOfWork;
            this._employeeContext = employeeContextService;
            this._InventoryService = inventoryService;

        }

        public async Task<string> Handle(AdjustInventoryCommand request, CancellationToken cancellationToken)
        {


            var productRepository = unitOfWork.GetRepository<Product, int>();

            var StockAdjustmentLogsRepository = unitOfWork.GetRepository<StockAdjustmentLog, int>();

            var employeeId = await _employeeContext.GetCurrentEmployeeIdAsync();




            var log = new StockAdjustmentLog
            {
                Reason = request.Reason,
                ProductId = request.ProductId,
                AdjustedById = employeeId,
                QuantityChange = request.QuantityChange,
            };



            await StockAdjustmentLogsRepository.AddAsync(log);


            await _InventoryService.AdjustStockAsync(

             new List<InventoryStockAdjustmentDto>()
             {

                new InventoryStockAdjustmentDto
                {
                    ProductId=request.ProductId,
                    QuantityChange=request.QuantityChange,
                }
             });



            return "Stock adjusted successfully.";

        }
    }
}
