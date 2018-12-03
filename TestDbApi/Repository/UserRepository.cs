using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDbApi.Models;
using TestDbApi.Interface;
using TestDbApi.Data;
using TestDbApi.Models.ExtendedModels;
using Microsoft.EntityFrameworkCore;
using TestDbApi.Models.Extensions;

namespace TestDbApi.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(TheCRMContext theCRMContext) : base(theCRMContext)
        {
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var user = await FindAllAsync();
            return user.OrderBy(us => us.Username);
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            var user = await FindByConditionAsync(u => u.Id.Equals(userId));
            return user.DefaultIfEmpty(new User()).FirstOrDefault();
        }

        public async Task<UserExtended> GetUserWithDetailsAsync(Guid userId)
        {
            var user = await GetUserByIdAsync(userId);
            return new UserExtended(user)
            {
                NumberCustomersCreated = await TheCRMContext.Users.Include(u => u.CustomersCreate).SelectMany(uc => uc.CustomersCreate).Where(cc => cc.CreatedById == userId).CountAsync(),
                NumberCustomersUpdated = await TheCRMContext.Users.Include(u => u.CustomersUpdate).SelectMany(uc => uc.CustomersUpdate).Where(cu => cu.UpdatedById == userId).CountAsync(),
                CustomerCreated = await TheCRMContext.Users.Include(u => u.CustomersCreate).SelectMany(uc => uc.CustomersCreate).Where(cc => cc.CreatedById == userId).Select(c => new UserCustomerDetails(){Name = c.Name, Surname = c.Surname }).ToListAsync(),
                CustomerUpdated = await TheCRMContext.Users.Include(u => u.CustomersUpdate).SelectMany(uc => uc.CustomersUpdate).Where(cu => cu.UpdatedById == userId).Select(c => new UserCustomerDetails(){Name = c.Name, Surname = c.Surname}).ToListAsync()
            };
        }

        public async Task<UserWithOutCustomerInfo> GetUserWithOutCustomerInfoAsync(Guid userId)
        {
            var user = await GetUserByIdAsync(userId);
            return new UserWithOutCustomerInfo(user);
        }

        public async Task<UserWithOutPass> GetUserWithOutPassAsync(Guid userId)
        {
            var user = await GetUserByIdAsync(userId);
            return new UserWithOutPass(user);
        }

        public async Task CreateUserAsync(User user)
        {
            user.Id = Guid.NewGuid();
            Create(user);
            await SaveAsync();
        }

        public async Task UpdateUserAsync(User dbUser, User user)
        {
            dbUser.Map(user);
            Update(dbUser);
            await SaveAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            Delete(user);
            await SaveAsync();
        }
    }
}
