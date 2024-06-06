using App.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities.Orders
{
    public class Order : IEntity, IAuditable, IArchivable
    {
        public Guid Id { get ; set; }

        [Index]
        public Guid CustomerId { get; set; }

        public OrderStatus Status { get; set; }

        [Index]
        public DateTime? CreatedOn { get ; set; }

        public DateTime? ModifiedOn { get; set ; }

        public virtual ICollection<Orderline> Orderlines { get; set; }

        public bool IsDeprecated { get ; set; }
    }

    public enum OrderStatus
    {
        Pending,
        Shipped,
        OnRoute,
        Delivered,
        Returned
    }
}
