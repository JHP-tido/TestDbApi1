using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDbApi.Models;

namespace TestDbApi.Interface
{
    public interface ILoginRepository:IRepositoryBase<User>
    {
        Task<User> Authenticate(Login login);
        string BuildToken(User user);
    }
}
