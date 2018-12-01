using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDbApi.Models;
using TestDbApi.Models.ExtendedModels;

namespace TestDbApi.Interface
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(Guid userId);
        UserExtended GetUserWithDetails(Guid userId);
        UserWithOutCustomerInfo GetUserWithOutCustomerInfo(Guid userId);
        UserWithOutPass GetUserWithOutPass(Guid userId);
        void CreateUser(User user);
        void UpdateUser(User dbUser, User user);
        void DeleteUser(User user);
    }
}
