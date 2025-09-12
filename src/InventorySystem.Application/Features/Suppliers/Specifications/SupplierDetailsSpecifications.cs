
using Domain.Entities;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Specifications.SupplierSpec
{
    public class SupplierDetailsSpecifications : ISpecification<Supplier>, IIncludeSpecification<Supplier>
    {
        public Expression<Func<Supplier, bool>> Criteria { get; }

        public List<Expression<Func<Supplier, object>>> IncludeExpressions { get; } = new();

        public bool IgnoreQueryFilters { get; set; } = false;

        public SupplierDetailsSpecifications(int id)
        {

            Criteria = (s => s.Id == id);

            IncludeExpressions.Add(s => s.SupplierCategories!);
        }
    }
}
