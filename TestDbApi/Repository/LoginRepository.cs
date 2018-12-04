using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDbApi.Models;
using TestDbApi.Interface;
using System.Text;
using TestDbApi.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TestDbApi.Repository
{
    public class LoginRepository:RepositoryBase<User>, ILoginRepository
    {
        private readonly IConfiguration _config;

        /*public LoginRepository(TheCRMContext theCRMContext) : base(theCRMContext)
        {
        }*/

        public LoginRepository(TheCRMContext theCRMContext, IConfiguration config): base(theCRMContext)
        {
            _config = config;
        }

        public async Task<User> Authenticate(Login login)
        {
            var user = await FindByConditionAsync(u => u.Username.Equals(login.Username) && u.Password.Equals(login.Password));
            return user.DefaultIfEmpty(new User()).FirstOrDefault();
        }

        public string BuildToken(User user)
        {
            try
            {
                //Change key in appsettings.json, needed size more than 128 bytes
                Console.WriteLine("_______________Entrada9CredentialsInit");
                Console.WriteLine("_____jwt:issuer: " + _config["Jwt:Issuer"]);
                Console.WriteLine("_____jwt:key: " + _config["Jwt:Key"]);
                //Here call the appsettings.json key
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                Console.WriteLine("_______________Entrada10CredentialsCreate");
            
                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);
                Console.WriteLine("_______________Entrada11CredentialsCreate");
                var value = new JwtSecurityTokenHandler().WriteToken(token);
                Console.WriteLine("__________Value: " + token);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch(Exception ex)
            {
                Console.WriteLine("_::_________Esta es la exception: " + ex.Message.ToString());
            }
            return("slfks");
        }
    }
}
