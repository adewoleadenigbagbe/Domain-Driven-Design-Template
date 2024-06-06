using App.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities.Customers
{
    public class Address : IEntity, IAuditable, IArchivable
    {
        public Guid Id { get ; set; }

        public string AddressLine { get; set; }

        public string City { get; set; }

        public string State { get; set; }

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
