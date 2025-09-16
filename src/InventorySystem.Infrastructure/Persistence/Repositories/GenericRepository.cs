using Domain.Specifications;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using InventorySystem.Application.Common.Interfaces.Repositories;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories
{
    internal class GenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : class
    {


        protected readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {

            _context = context;
        }


        //Add - Update - Delete
        public async Task AddAsync(T item)
        {



            await _context.Set<T>().AddAsync(item);
        }

        public void Delete(T item)
        {

            _context.Set<T>().Remove(item);
        }



        public void Update(T entity)
        {


            _context.Update(entity);
        }

        //----------------------------------------------------------------------------




        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AsNoTracking().AnyAsync(predicate);
        }




        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, bool AsNoTracking = true)
        {

            if (AsNoTracking)
                return await _context.Set<T>().AsNoTracking().Where(predicate).ToListAsync();

            return await _context.Set<T>().Where(predicate).ToListAsync();
        }



        public IQueryable<T> GetAllAsIQueryableAsync(bool AsNoTracking = true)
        {

            if (AsNoTracking)
                return _context.Set<T>().AsNoTracking();


            return _context.Set<T>();
        }



        public async Task<IEnumerable<T>> GetAllAsync(bool AsNoTracking = true)
        {


            if (AsNoTracking)
                return await _context.Set<T>().AsNoTracking().ToListAsync();


            return await _context.Set<T>().ToListAsync();
        }


        //-------------------------------------------------------

        //get only one

        public async Task<T?> GetById(TKey id) => await _context.Set<T>().FindAsync(id);



        public async Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T> specification, Expression<Func<T, TResult>> selector, bool asNoTracking = true)
        {
            var query = ApplySpecifications(specification);


            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.Select(selector).FirstOrDefaultAsync();

        }


        public async Task<T?> FirstOrDefaultAsync(ISpecification<T> specification, bool asNoTracking = true)
        {
            var query = ApplySpecifications(specification);


            if (asNoTracking)
                query = query.AsNoTracking();


            return await query.FirstOrDefaultAsync();


        }





        public async Task<TResult?> FirstOrDefaultAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, bool asNoTracking = true)
        {


            if (asNoTracking)
                return await _context.Set<T>().AsNoTracking().Where(predicate).Select(selector).FirstOrDefaultAsync();



            return await _context.Set<T>().Where(predicate).Select(selector).FirstOrDefaultAsync();

        }



        //----------------------------------------------------------------------



        public async Task<IEnumerable<T>> GetAllWithSpecifications(ISpecification<T> specification, bool AsNoTracking = true)
        {


            if (AsNoTracking)
            {

                return await ApplySpecifications(specification).AsNoTracking().ToListAsync();
            }


            return await ApplySpecifications(specification).ToListAsync();
        }











        public async Task<List<TResult>> ListAsync<TResult>(ISpecification<T> specifications, Expression<Func<T, TResult>> selector, bool asNoTracking = true)
        {


            var query = ApplySpecifications(specifications);

            if (asNoTracking)
                query = query.AsNoTracking();


            return await query.Select(selector).ToListAsync();

        }





        public async Task<List<TResult>> ListAsync<TResult>(Expression<Func<T, TResult>> selector, bool asNoTracking = true)
        {



            if (asNoTracking)
                return await _context.Set<T>().AsNoTracking().Select(selector).ToListAsync();


            return await _context.Set<T>().Select(selector).ToListAsync();
        }








        public async Task<List<TResult>> ListAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, bool asNoTracking = true)
        {



            if (asNoTracking)
                return await _context.Set<T>().AsNoTracking().Where(predicate).Select(selector).ToListAsync();


            return await _context.Set<T>().Where(predicate).Select(selector).ToListAsync();
        }



        private IQueryable<T> ApplySpecifications(ISpecification<T> specification)
        {


            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), specification);
        }


        private IQueryable<TResult> ApplyProjectionSpecifications<TResult>(IProjectionSpecifications<T, TResult> specifications)
        {

            return ProjectionSpecificationEvaluator<T, TResult>.GetQuery(_context.Set<T>(), specifications);

        }

        public async Task<IEnumerable<TResult>> GetAllWithProjectionSpecifications<TResult>(IProjectionSpecifications<T, TResult> specifications, bool AsNoTracking = true)
        {
            var query = ApplyProjectionSpecifications(specifications);


            var x = await query.ToListAsync();


            return x;


        }


        public async Task<TResult> GetByIdWithProjectionSpecifications<TResult>(IProjectionSpecifications<T, TResult> specifications, bool AsNoTracking = true)
        {
            var query = ApplyProjectionSpecifications(specifications);


            var x = await query.FirstOrDefaultAsync();


            return x;


        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {

            return await _context.Set<T>().CountAsync(predicate);
        }

        public void UpdateRange(IEnumerable<T> items)
        {

            _context.UpdateRange(items);
        }


        private IQueryable<TResult> ApplyGroupingSpecifications<TResult, Key>(IQueryable<T> query, IGroupSpecifications<T, Key, TResult> specification)
        {



            return GroupSpecificationEvaluator<T, Key, TResult>.GetQuery(query, specification);
        }

        public async Task<IEnumerable<TResult>> GetAllWithGrouping<TResult, Key>(GroupSpecification<T, Key, TResult> specification)
        {


            var x = await ApplyGroupingSpecifications(_context.Set<T>(), specification).ToListAsync(); ;
            return x;
        }

        public async Task AddRangeAsync(IEnumerable<T> entites)
        {

            await _context.AddRangeAsync(entites);
        }
    }
}
