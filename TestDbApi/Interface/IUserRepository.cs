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
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(Guid userId);
        Task<UserExtended> GetUserWithDetailsAsync(Guid userId);
        Task<UserWithOutCustomerInfo> GetUserWithOutCustomerInfoAsync(Guid userId);
        Task<UserWithOutPass> GetUserWithOutPassAsync(Guid userId);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User dbUser, User user);
        Task DeleteUserAsync(User user);
    }
}
