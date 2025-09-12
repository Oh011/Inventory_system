using Domain.Entities;
using Domain.Specifications;
using Project.Application.Common.Enums.SortOptions;
using Project.Application.Features.Suppliers.Queries.GetSuppliers;

namespace Project.Application.Features.Suppliers.Specifications
{
    public class SupplierSpecifications : BaseSpecifications<Supplier>
    {


        public SupplierSpecifications(GetSuppliersQuery query) : base(


             s =>
            (string.IsNullOrEmpty(query.Name) || s.Name.ToLower().Contains(query.Name.ToLower())) &&
            (string.IsNullOrEmpty(query.Email) || (s.Email != null && s.Email.ToLower().Contains(query.Email.ToLower()))) &&
            (string.IsNullOrEmpty(query.ContactName) || (s.ContactName != null && s.ContactName.ToLower().Contains(query.ContactName.ToLower()))) &&
            (string.IsNullOrEmpty(query.Phone) || (s.Phone != null && s.Phone.ToLower().Contains(query.Phone.ToLower()))) &&
            (!query.CategoryId.HasValue || s.SupplierCategories!.Any(sc => sc.CategoryId == query.CategoryId)) &&
            (!query.CreationDate.HasValue || s.CreatedAt.Date == query.CreationDate.Value.Date)



            )
        {






            switch (query.SortOptions)
            {
                case SupplierSortOptions.NameAsc:
                    SetOrderBy(s => s.Name);
                    break;
                case SupplierSortOptions.NameDesc:
                    SetOrderByDescending(s => s.Name);
                    break;
                case SupplierSortOptions.CreatedAtAsc:
                    SetOrderBy(s => s.CreatedAt);
                    break;
                case SupplierSortOptions.CreatedAtDesc:
                default:
                    SetOrderByDescending(s => s.CreatedAt);
                    break;
            }


            ApplyPagination(query.PageIndex, query.pageSize);

        }








    }
}

