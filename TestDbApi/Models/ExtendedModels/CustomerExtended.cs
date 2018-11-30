using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestDbApi.Models.ExtendedModels
{
    public class CustomerExtended:IEntity
    {
        public Guid Id { get; set; }
        //public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Image { get; set; }
 
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public CustomerExtended()
        {
        }
 
        public CustomerExtended(Customer customer)
        {
            Id = customer.Id;
            //CustomerId = customer.CustomerId;
            Name = customer.Name;
            Surname = customer.Surname;
            Image = customer.Image;
        }
    }
}
