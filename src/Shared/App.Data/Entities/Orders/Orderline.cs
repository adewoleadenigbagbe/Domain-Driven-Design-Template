using App.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities.Orders
{
    public class Orderline : IEntity, IAuditable, IArchivable
    {
        public Guid Id { get; set; }

        [Index]
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Vat { get; set; }

        public virtual Order Order { get; set; }

        [ForeignKey("Order")]
        public Guid OrderId { get; set; }

        public bool IsDeprecated { get; set ; }

        [Index]
        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get ; set; }
    }
}
