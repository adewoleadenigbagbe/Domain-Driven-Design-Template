using App.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities.Customers
{
    public class Address : IEntity, IAuditable, IArchivable
    {
        public Guid Id { get ; set; }

        [MaxLength(100)]
        [Required]
        public string AddressLine { get; set; }

        [MaxLength(100)]
        [Required]
        public string City { get; set; }

        [MaxLength(100)]
        [Required]
        public string State { get; set; }

        [MaxLength(10)]
        [Required]
        public string PostalCode { get; set; }

        public virtual Customer Customer { get; set; }


        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }

        [Index]
        public DateTime? CreatedOn { get ; set ; }

        public DateTime? ModifiedOn { get; set ; }

        public bool IsDeprecated { get; set ; }
    }
}
