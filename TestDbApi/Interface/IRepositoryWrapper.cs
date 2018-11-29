using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestDbApi.Interface
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        ICustomerRepository Customer { get; }
    }
}
