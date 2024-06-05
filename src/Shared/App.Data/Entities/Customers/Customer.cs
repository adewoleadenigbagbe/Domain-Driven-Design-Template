using App.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
    public class Customer : IEntity, IAuditable, IArchivable
    {
        public Guid Id { get ; set ; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Dob { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get ; set ; }

        public bool IsDeprecated { get; set; }
    }
}
