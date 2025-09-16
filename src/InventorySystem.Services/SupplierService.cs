
using Application.Exceptions;
using Application.Specifications.SupplierSpec;
using Domain.Entities;
using InventorySystem.Application.Common.Interfaces.Repositories;
using InventorySystem.Application.Common.Validators;
using InventorySystem.Application.Features.Categories.Dtos;
using InventorySystem.Application.Features.Suppliers.Dtos;
using InventorySystem.Application.Features.Suppliers.Interfaces;
using InventorySystem.Application.Features.Suppliers.Queries.GetSuppliers;
using InventorySystem.Application.Features.Suppliers.Specifications;
using Shared.Results;

namespace InventorySystem.Services
{
    internal class SupplierService(IUnitOfWork unitOfWork, ISupplierRepository supplierRepository, IEntityValidator<Category> categoryValidator) : ISupplierService
    {
        public async Task<Supplier> CreateSupplier(Supplier supplier)
        {




            if (supplier.SupplierCategories != null)
            {

                var categoryIds = supplier.SupplierCategories?.Select(sc => sc.CategoryId).ToList() ?? [];

                await categoryValidator.ValidateExistAsync(categoryIds, "categories");


                supplier.SupplierCategories = categoryIds.Select(id => new SupplierCategory
                {
                    CategoryId = id
                }).ToList();

            }





            var supplierRepository = unitOfWork.GetRepository<Supplier, int>();


            await supplierRepository.AddAsync(supplier);

            await unitOfWork.Commit();


            return supplier;



        }

        public async Task<PaginatedResult<Supplier>> GetAllSuppliers(GetSuppliersQuery query)
        {


            var repository = unitOfWork.GetRepository<Supplier, int>();

            var specifications = new SupplierSpecifications(query);

            var result = await repository.GetAllWithSpecifications(specifications);

            int totalCount = await repository.CountAsync(specifications.Criteria);

            return new PaginatedResult<Supplier>(


                 query.PageIndex,
                query.pageSize,
                 totalCount,
                result
            );


        }

        public async Task<SupplierBriefDto> GetSupplierBrief(int id)
        {

            var supplierRepository = unitOfWork.GetRepository<Supplier, int>();


            var supplier = await supplierRepository.FirstOrDefaultAsync(s => s.Id == id, s => new SupplierBriefDto
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email
            });

            if (supplier == null)
            {
                throw new NotFoundException("Supplier Not found");
            }


            return supplier;
        }

        public async Task<SupplierDetailDto> GetSupplierById(int id)
        {

            var repository = unitOfWork.GetRepository<Supplier, int>();



            var Supplier = (await supplierRepository.GetSupplierWithDetailsAsync(id));



            if (Supplier == null)
            {


                throw new NotFoundException($"Supplier with Id :{id} is not found");
            }

            var SupplierDetails = new SupplierDetailDto
            {

                Id = Supplier.Id,
                Name = Supplier.Name,
                ContactName = Supplier.ContactName,
                Email = Supplier.Email,
                Address = Supplier.Address,
                Phone = Supplier.Phone,
                CreatedAt = Supplier.CreatedAt,

                Categories = Supplier.SupplierCategories!
                        .Select(sc => sc.Category)
                        .Select(c => new CategoryDto { Id = c.Id, Name = c.Name })
                        .ToList()

            };






            return SupplierDetails;


        }

        public async Task<Supplier> UpdateSupplier(Supplier entity)
        {

            var repository = unitOfWork.GetRepository<Supplier, int>();
            var supplier = await repository.FirstOrDefaultAsync(

                new SupplierDetailsSpecifications(entity.Id)
                );



            if (supplier == null)
                throw new NotFoundException($"Supplier with ID {entity.Id} not found.");


            if (entity.Name != null)
                supplier.Name = entity.Name;

            if (entity.ContactName != null)
                supplier.ContactName = entity.ContactName;

            if (entity.Phone != null)
                supplier.Phone = entity.Phone;

            if (entity.Email != null)
                supplier.Email = entity.Email;

            if (entity.Address != null)
                supplier.Address = entity.Address;



            if (entity.SupplierCategories != null)
            {
                var categoryIds = entity.SupplierCategories?.Select(sc => sc.CategoryId).Distinct().ToList() ?? [];



                await categoryValidator.ValidateExistAsync(categoryIds, "categories");

                supplier.SyncCategories(categoryIds);

            }


            repository.Update(supplier);

            await unitOfWork.Commit();


            return supplier;



        }







    }
}
