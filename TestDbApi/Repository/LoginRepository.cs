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
                var claims = new[] {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, Enum.GetName(typeof(Roles), user.Role)),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };
                //Change key in appsettings.json, needed size more than 128 bytes
                //Issuer: Only http://localhost:63383/ send you valid token, configure appsettings.json
                //for you debug URL local or configure your project to 63383 or configure both for local URL that you want
                //Here call the appsettings.json key
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

                var value = new JwtSecurityTokenHandler().WriteToken(token);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch(Exception ex)
            {
                Console.WriteLine("_::_________Error: " + ex.Message.ToString());
                return ex.Message.ToString();
            }
        }
    }
}
