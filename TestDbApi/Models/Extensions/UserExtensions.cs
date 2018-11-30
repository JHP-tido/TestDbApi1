using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestDbApi.Models.Extensions
{
    public static class UserExtensions
    {
        public static void Map(this User dbUser, User user)
        {
            dbUser.Username = user.Username;
            dbUser.Password = user.Password;
            dbUser.Role = user.Role;
        }
    }
}
