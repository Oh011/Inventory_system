using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public abstract class RepositoryBase<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        protected RepositoryBase(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
    }

}
