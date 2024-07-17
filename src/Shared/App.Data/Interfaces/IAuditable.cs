using App.Data.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Interfaces
{
    public interface IAuditable
    {
        [AuditableCreatedOn]
        DateTime? CreatedOn { get; set; }

        [AuditableModifiedOn]
        DateTime? ModifiedOn { get; set; }
    }
}
