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
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomerById(Guid customerId);
        CustomerExtended GetCustomerWithDetails(Guid customerId);
        string GetCustomerImage(Guid customerId);
        //Modify for delete on cascade in database and remove this code
        IEnumerable<Customer> CustomersByUser(Guid userId);
    }
}
