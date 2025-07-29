using Domain.Entities;
using Domain.Specifications;
using Project.Application.Features.Categories.Dtos;

namespace Project.Application.Features.Suppliers.Specifications
{
    internal class SupplierCategoriesSpecifications : ProjectionSpecifications<SupplierCategory, CategoryDto>
    {


        public SupplierCategoriesSpecifications(int id) : base(s => s.SupplierId == id)
        {



            AddInclude(s => s.Category);

            AddProjection(s => new CategoryDto
            {

                Id = s.Category.Id,
                Name = s.Category.Name,
            });

        }
    }
}
