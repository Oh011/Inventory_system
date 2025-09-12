
using Domain.Entities;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Specifications.CategorySpec
{
    public class CategoryByNameSpecification : ISpecification<Category>
    {
        public Expression<Func<Category, bool>> Criteria { get; }

        public bool IgnoreQueryFilters { get; set; } = false;

        public CategoryByNameSpecification(string? name)
        {
            Criteria = c => (string.IsNullOrEmpty(name)
    || c.Name.ToLower().Contains(name.Trim().ToLower()) && c.IsDeleted == false);

        }
    }
}
