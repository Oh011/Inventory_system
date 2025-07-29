using AutoMapper;
using Domain.Entities;
using MediatR;
using Project.Application.Features.Suppliers.Dtos;
using Project.Application.Features.Suppliers.Interfaces;

namespace Project.Application.Features.Suppliers.Commands.Update
{
    internal class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierRequest, SupplierDto>
    {

        private readonly ISupplierService _supplierService;

        private readonly IMapper _mapper;


        public UpdateSupplierCommandHandler(ISupplierService supplierService, IMapper mapper)
        {
            this._mapper = mapper;
            this._supplierService = supplierService;
        }
        public async Task<SupplierDto> Handle(UpdateSupplierRequest request, CancellationToken cancellationToken)
        {


            var supplier = new Supplier
            {
                Id = request.Id,
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



            var result = await _supplierService.UpdateSupplier(supplier);



            return _mapper.Map<SupplierDto>(result);
        }
    }
}
