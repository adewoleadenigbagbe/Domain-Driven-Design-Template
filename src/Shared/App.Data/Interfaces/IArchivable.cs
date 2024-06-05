using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Interfaces
{
    public interface IArchivable
    {
        bool IsDeprecated { get;set;}
    }
}
