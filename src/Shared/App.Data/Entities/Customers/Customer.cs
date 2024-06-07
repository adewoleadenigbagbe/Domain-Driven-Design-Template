using App.Data.Entities.Customers;
using App.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
    public class Customer : IEntity, IAuditable, IArchivable
    {
        public Guid Id { get ; set ; }

        [MaxLength(100)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(100)]
        [Required]
        public string LastName { get; set; }

        public DateTime Dob { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get ; set ; }

        public bool IsDeprecated { get; set; }
    }
}
