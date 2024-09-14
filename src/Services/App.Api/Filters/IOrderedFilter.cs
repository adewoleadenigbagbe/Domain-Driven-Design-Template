using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace App.Api.Filters
{
    public interface IOrderedFilter : IFilter
    {
        int Order { get; } 
    }
}
