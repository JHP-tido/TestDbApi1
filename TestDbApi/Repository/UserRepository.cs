using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDbApi.Models;
using TestDbApi.Interface;
using TestDbApi.Data;
using TestDbApi.Models.ExtendedModels;
using Microsoft.EntityFrameworkCore;

namespace TestDbApi.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(TheCRMContext theCRMContext) : base(theCRMContext)
        {
        }

        public IEnumerable<User> GetAllUsers()
        {
            return FindAll().OrderBy(us => us.Username);
        }

        public User GetUserById(Guid userId)
        {
            return FindByCondition(user => user.Id.Equals(userId))
                .DefaultIfEmpty(new User())
                .FirstOrDefault();
        }

        public UserExtended GetUserWithDetails(Guid userId)
        {
            return new UserExtended(GetUserById(userId))
            {
                CustomerCreated = TheCRMContext.Users.Include(u => u.CustomersCreate).SelectMany(uc => uc.CustomersCreate).Select(c => new UserCustomerDetails(){Name = c.Name, Surname = c.Surname }).ToList(),
                CustomerUpdated = TheCRMContext.Users.Include(u => u.CustomersUpdate).SelectMany(uc => uc.CustomersUpdate).Select(c => new UserCustomerDetails(){Name = c.Name, Surname = c.Surname}).ToList()
            };
        }

        public UserWithOutCustomerInfo GetUserWithOutCustomerInfo(Guid userId)
        {
            return new UserWithOutCustomerInfo(GetUserById(userId));
        }

        public UserWithOutPass GetUserWithOutPass(Guid userId)
        {
            return new UserWithOutPass(GetUserById(userId));
        }

        public void CreateUser(User user)
        {
            user.Id = Guid.NewGuid();
            Create(user);
            Save();
        }
    }
}
