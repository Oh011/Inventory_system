
using Domain.Entities;
using MediatR;
using Project.Application.Common.Interfaces.Repositories;
using Project.Application.Features.Categories.Dtos;
using Project.Application.Features.Suppliers.Specifications;

namespace Project.Application.Features.Suppliers.Queries.GetSuppliersCategories
{
    internal class GetSupplierCategoriesQueryHandler : IRequestHandler<GetSupplierCategoriesQuery, IEnumerable<CategoryDto>>
    {



        private readonly IUnitOfWork _unitOfWork;


        public GetSupplierCategoriesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(GetSupplierCategoriesQuery request, CancellationToken cancellationToken)
        {


            var repository = _unitOfWork.GetRepository<SupplierCategory, int>();

            var specifications = new SupplierCategoriesSpecifications(request.SupplierId);
            var categories = await repository.GetAllWithProjectionSpecifications(specifications);


            return categories;

        }
    }
}
