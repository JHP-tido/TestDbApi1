using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDbApi.Models;

namespace TestDbApi.Data
{
    public class TheCRMContext:DbContext
    {
        public TheCRMContext(DbContextOptions<TheCRMContext> options) : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new { Id = Guid.NewGuid(), Username = "admin", Password = "admin", Role = Roles.admin },
                new { Id = Guid.NewGuid(), Username = "user1", Password = "1234", Role = Roles.user });
        }

        //Lazy loader https://docs.microsoft.com/en-us/ef/core/querying/related-data#lazy-loading
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
