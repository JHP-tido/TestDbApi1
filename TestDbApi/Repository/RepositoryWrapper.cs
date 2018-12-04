using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDbApi.Interface;
using TestDbApi.Data;
using Microsoft.Extensions.Configuration;

namespace TestDbApi.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private TheCRMContext _crmContext;
        private IUserRepository _user;
        private ICustomerRepository _customer;
        private ILoginRepository _login;
        private IConfiguration _config;

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_crmContext);
                }

                return _user;
            }
        }

        public ICustomerRepository Customer
        {
            get
            {
                if (_customer == null)
                {
                    _customer = new CustomerRepository(_crmContext);
                }

                return _customer;
            }
        }

        public ILoginRepository Login
        {
            get
            {
                if (_login == null)
                {
                    _login = new LoginRepository(_crmContext,_config);
                }

                return _login;
            }
        }

        public RepositoryWrapper(TheCRMContext theCRMContext, IConfiguration config)
        {
            _crmContext = theCRMContext;
            _config = config;
        }
    }
}
