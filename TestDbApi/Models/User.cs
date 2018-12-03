﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestDbApi.Models
{
    //Lazy Loader https://docs.microsoft.com/en-us/ef/core/querying/related-data#lazy-loading
    [Table("user")]
    public class User:IEntity
    {
        [Key]
        [Column("UserId")]
        public Guid Id { get; set; }
        //public Guid UserId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, ErrorMessage = "Username cannot be longer than 50 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "Password cannot be longer than 100 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public Roles Role { get; set; }

        [InverseProperty("CreatedBy")]
        public virtual ICollection<Customer> CustomersCreate { get; set; }
        [InverseProperty("UpdatedBy")]
        public virtual ICollection<Customer> CustomersUpdate { get; set; }

        //[InverseProperty("CreatedBy")]
        //public List<Customer> CustomersCreated { get; set; }
        //[InverseProperty("UpdatedBy")]
        //public List<Customer> CustomersUpdated { get; set; }
        //public ICollection<Customer> Customers { get; set; }
        /*[InverseProperty("CreatedBy")]
        public List<Customer> CustomersCreated { get; set; }
        [InverseProperty("UpdatedBy")]
        public List<Customer> CustomersUpdated { get; set; }*/
    }

    public enum Roles
    {
        admin,
        user,
    }
}
