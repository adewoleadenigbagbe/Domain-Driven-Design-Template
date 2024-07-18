using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity.Infrastructure;

namespace App.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public abstract class PreSaveActionAttribute : Attribute
    {
        public abstract bool CanPerformAction(DbEntityEntry entry, Type propertyType);

        public abstract object PerformAction(DbEntityEntry entry, object originalValue, object currentValue);
    }
}
