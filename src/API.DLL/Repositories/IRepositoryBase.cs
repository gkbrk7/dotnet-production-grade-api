using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using API.DLL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace API.DLL.Repositories
{
    public interface IRepositoryBase<T> where T : class, new()
    {
        Task<IQueryable<T>> QueryAll(Expression<Func<T, bool>> expression = null);
        Task<List<T>> GetList(Expression<Func<T, bool>> expression = null);
        Task CreateAsync(T entry);
        Task CreateRangeAsync(List<T> entryList);
        void UpdateAsync(T entry);
        void UpdateRange(List<T> entryList);
        void DeleteAsync(T entry);
        void DeleteRangeAsync(List<T> entryList);
        Task<T> FindSingleAsync(Expression<Func<T, bool>> expression);
        Task<bool> SaveCompletedAsync();
    }

    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, new()
    {
        private readonly ApplicationDbContext context;

        public RepositoryBase(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(T entry)
        {
            await context.Set<T>().AddAsync(entry);
        }

        public async Task CreateRangeAsync(List<T> entryList)
        {
            await context.Set<T>().AddRangeAsync(entryList);
        }

        public void DeleteAsync(T entry)
        {
            context.Set<T>().Remove(entry);
        }

        public void DeleteRangeAsync(List<T> entryList)
        {
            context.Set<T>().RemoveRange(entryList);
        }

        public async Task<T> FindSingleAsync(Expression<Func<T, bool>> expression)
        {
            return await context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<List<T>> GetList(Expression<Func<T, bool>> expression = null)
        {
            return await (expression != null ? context.Set<T>().AsQueryable().Where(expression).AsNoTracking().ToListAsync() : context.Set<T>().AsQueryable().AsNoTracking().ToListAsync());
        }

        public async Task<IQueryable<T>> QueryAll(Expression<Func<T, bool>> expression = null)
        {
            return await Task.FromResult(expression != null ? context.Set<T>().AsQueryable().Where(expression).AsNoTracking() : context.Set<T>().AsQueryable().AsNoTracking());
        }

        public async Task<bool> SaveCompletedAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void UpdateAsync(T entry)
        {
            context.Set<T>().Update(entry);
        }

        public void UpdateRange(List<T> entryList)
        {
            context.Set<T>().UpdateRange(entryList);
        }
    }
}