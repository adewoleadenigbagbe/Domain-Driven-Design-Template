using App.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
    public class Product : IEntity, IAuditable, IArchivable
    {
        public Guid Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal Vat { get; set; }

        public DateTime? CreatedOn { get; set ; }

        public DateTime? ModifiedOn { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsDeprecated { get ; set ; }
    }
}
