using App.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;

namespace App.Data.Attributes
{
    public class SequentialGuidAttribute : PreSaveActionAttribute
    {
        public override bool CanPerformAction(DbEntityEntry entry, Type propertyType)
        {
            return propertyType == typeof(Guid);
        }

        public override object PerformAction(DbEntityEntry entry, object originalValue, object currentValue)
        {
            if ( entry.State != EntityState.Added)
            {
                throw new InvalidOperationException("Entity already existed and state cannnot be changed");
            }

            var guid = (Guid)currentValue;
            if (guid == default)
            {
                guid = SequentialGuid.Create();
            }

           return guid;
        }
    }
}
