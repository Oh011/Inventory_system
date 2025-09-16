using AutoMapper;
using Domain.Entities;
using MediatR;
using InventorySystem.Application.Features.Suppliers.Dtos;
using InventorySystem.Application.Features.Suppliers.Interfaces;

namespace InventorySystem.Application.Features.Suppliers.Commands.Create
{
    internal class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, SupplierDto>
    {

        private readonly ISupplierService supplierService;

        private readonly IMapper _mapper;


        public CreateSupplierCommandHandler(ISupplierService supplierService, IMapper mapper)
        {
            this.supplierService = supplierService;
            this._mapper = mapper;
        }

        public async Task<SupplierDto> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {


            var supplier = new Supplier
            {
                Name = request.Name,
                ContactName = request.ContactName,
                Phone = request.Phone,
                Email = request.Email,
                Address = request.Address,

                SupplierCategories = request.CategoryIds != null && request.CategoryIds.Any() ?
               request.CategoryIds?.Select(id => new SupplierCategory
               {
                   CategoryId = id
               }).ToList() : null

            };


            var result = await supplierService.CreateSupplier(supplier);



            return _mapper.Map<SupplierDto>(result);


        }
    }
}
