using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDbApi.Models;
using TestDbApi.Models.ExtendedModels;

namespace TestDbApi.Interface
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {  
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(Guid customerId);
        Task<CustomerExtended> GetCustomerWithDetailsAsync(Guid customerId);
        Task<string> GetCustomerImageAsync(Guid customerId);
        //Modify for delete on cascade in database and remove this code
        Task<IEnumerable<Customer>> CustomersByUserAsync(Guid userId);
        Task<CustomerWithUsersId> GetCustomersWithUsersIdAsync(Guid customerId);
    }
}
