using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDbApi.Interface;
using TestDbApi.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace TestDbApi.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected TheCRMContext TheCRMContext { get; set; }
        
        public RepositoryBase(TheCRMContext theCRMContext)
        {
            TheCRMContext = theCRMContext;
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await TheCRMContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await TheCRMContext.Set<T>().Where(expression).ToListAsync();
        }

        public void Create(T entity)
        {
            TheCRMContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            TheCRMContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            TheCRMContext.Set<T>().Remove(entity);
        }

        public async Task SaveAsync()
        {
            await TheCRMContext.SaveChangesAsync();
        }
    }
}
