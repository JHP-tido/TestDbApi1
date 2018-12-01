using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDbApi.Interface;
using TestDbApi.Models;
using TestDbApi.Data;
using TestDbApi.Models.ExtendedModels;
using Microsoft.EntityFrameworkCore;

namespace TestDbApi.Repository
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(TheCRMContext theCRMContext) : base(theCRMContext)
        {
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return FindAll().OrderBy(cu => cu.Name);
        }

        public Customer GetCustomerById(Guid customerId)
        {
            return FindByCondition(customer => customer.Id.Equals(customerId))
                .DefaultIfEmpty(new Customer())
                .FirstOrDefault();
        }

        public CustomerExtended GetCustomerWithDetails(Guid customerId)
        {
            return new CustomerExtended(GetCustomerById(customerId))
            {
                CreatedBy = TheCRMContext.Customers.Include(u => u.CreatedBy).Where(c => c.Id == customerId).FirstOrDefault().CreatedBy.Username,
                UpdatedBy = TheCRMContext.Customers.Include(u => u.UpdatedBy).Where(c => c.Id == customerId).FirstOrDefault().UpdatedBy.Username
            };
        }

        public string GetCustomerImage(Guid customerId)
        {
            return GetCustomerById(customerId).Image;
        }

        //Modify for delete on cascade in database and remove this code
        //Charge Lazy loader https://docs.microsoft.com/en-us/ef/core/querying/related-data#lazy-loading
        public IEnumerable<Customer> CustomersByUser(Guid userId)
        {
            var create = FindByCondition(a => a.CreatedById.Equals(userId));
            if (create.Any())
            {
                Console.WriteLine("_________________Entra 3");
                return create;
            }
            else
            {
                Console.WriteLine("_________________Entra 4");
                return FindByCondition(a => a.UpdatedById.Equals(userId));
            }
            /*var Create = FindByCondition(a => a.CreatedBy.Id.Equals(userId));
            if(Create.Any())
            {
                return Create;
            }
            else
            {
                return FindByCondition(a => a.UpdatedBy.Id.Equals(userId));
            }*/
        }
    }
}
