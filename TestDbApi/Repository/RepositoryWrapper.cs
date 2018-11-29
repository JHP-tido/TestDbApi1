using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDbApi.Interface;
using TestDbApi.Data;

namespace TestDbApi.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private TheCRMContext _crmContext;
        private IUserRepository _user;
        private ICustomerRepository _customer;

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

        public RepositoryWrapper(TheCRMContext theCRMContext)
        {
            _crmContext = theCRMContext;
        }
    }
}
