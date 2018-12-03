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

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            var customer = await FindAllAsync();
            return customer.OrderBy(cu => cu.Name);
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid customerId)
        {
            var customer = await FindByConditionAsync(cu => cu.Id.Equals(customerId));
            return customer.DefaultIfEmpty(new Customer()).FirstOrDefault();
        }

        public async Task<CustomerExtended> GetCustomerWithDetailsAsync(Guid customerId)
        {
            var customer = await GetCustomerByIdAsync(customerId);
            var createdByQ = await TheCRMContext.Customers.Include(u => u.CreatedBy).Where(c => c.Id == customerId).FirstOrDefaultAsync();
            var updatedByQ = await TheCRMContext.Customers.Include(u => u.UpdatedBy).Where(c => c.Id == customerId).FirstOrDefaultAsync();
            return new CustomerExtended(customer)
            {
                CreatedBy = createdByQ.CreatedBy.Username,
                UpdatedBy = updatedByQ.UpdatedBy.Username
            };
        }

        public async Task<string> GetCustomerImageAsync(Guid customerId)
        {
            var customer = await GetCustomerByIdAsync(customerId);
            return customer.Image;
        }

        //Modify for delete on cascade in database and remove this code
        //Charge Lazy loader https://docs.microsoft.com/en-us/ef/core/querying/related-data#lazy-loading
        public async Task<IEnumerable<Customer>> CustomersByUserAsync(Guid userId)
        {
            var create = await FindByConditionAsync(a => a.CreatedById.Equals(userId));
            if (create.Any())
            {
                return create;
            }
            else
            {
                return await FindByConditionAsync(a => a.UpdatedById.Equals(userId));
            }
        }

        public async Task<CustomerWithUsersId> GetCustomersWithUsersIdAsync(Guid customerId)
        {
            var customer = await GetCustomerByIdAsync(customerId);
            return new CustomerWithUsersId(customer);
        }
    }
}
