using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDbApi.Interface;
using TestDbApi.Data;
using System.Linq.Expressions;

namespace TestDbApi.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected TheCRMContext TheCRMContext { get; set; }
        
        public RepositoryBase(TheCRMContext theCRMContext)
        {
            TheCRMContext = theCRMContext;
        }

        public IEnumerable<T> FindAll()
        {
            return TheCRMContext.Set<T>();
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return TheCRMContext.Set<T>().Where(expression);
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

        public void Save()
        {
            TheCRMContext.SaveChanges();
        }
    }
}
