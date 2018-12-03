using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestDbApi.Models
{
    //Lazy Loader https://docs.microsoft.com/en-us/ef/core/querying/related-data#lazy-loading
    [Table("customer")]
    public class Customer:IEntity
    {
        [Key]
        [Column("CustomerId")]
        public Guid Id { get; set; }
        //public Guid CustomerId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "Surname cannot be longer than 50 characters")]
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }

        [StringLength(100, ErrorMessage = "Image URL cannot be longer than 100 characters")]
        public string Image { get; set; }

        //public Guid CreatedById { get; set; }
        //public Guid UpdatedById { get; set;}
        [ForeignKey("CreatedBy")]
        public Guid? CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }
        
        [ForeignKey("UpdatedBy")]
        public Guid? UpdatedById { get; set; }
        public virtual User UpdatedBy { get; set; }
    }
}
