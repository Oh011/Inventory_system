using Domain.Entities;
using Domain.Specifications;
using InventorySystem.Application.Common.Enums.SortOptions;
using InventorySystem.Application.Features.Customers.Queries.GetAllCustomers;

namespace InventorySystem.Application.Features.Customers.Specifications
{
    public class CustomerSpecifications : BaseSpecifications<Customer>
    {



        public CustomerSpecifications(GetCustomersQuery query)


        : base(c => (query.IsDeleted == null || c.IsDeleted == query.IsDeleted) &&
    (string.IsNullOrWhiteSpace(query.Name) || c.FullName.Contains(query.Name)) &&
    (string.IsNullOrWhiteSpace(query.Phone) || (c.Phone != null && c.Phone.Contains(query.Phone))) &&
    (string.IsNullOrWhiteSpace(query.Email) || (c.Email != null && c.Email.Contains(query.Email))) &&
    (string.IsNullOrWhiteSpace(query.Address) || (c.Address != null && c.Address.Contains(query.Address)))





        )

        {



            switch (query.CustomerSortOptions)
            {
                case CustomerSortOptions.FullNameAsc:
                    SetOrderBy(c => c.FullName);
                    break;

                case CustomerSortOptions.FullNameDesc:
                    SetOrderByDescending(c => c.FullName);
                    break;


                default:
                    SetOrderBy(c => c.FullName); // default fallback
                    break;
            }


            ApplyPagination(query.PageIndex, query.pageSize);







        }

    }
}
