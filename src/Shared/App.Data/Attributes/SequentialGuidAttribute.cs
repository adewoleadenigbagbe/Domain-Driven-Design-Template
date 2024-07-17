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
        public override bool CanPerformAction(DbEntityEntry entry, string name)
        {
            var propertyType = entry.Property(name).GetType();

            return propertyType == typeof(Guid);
        }

        public override object PerformAction(DbEntityEntry entry, string name)
        {
            if ( entry.State != EntityState.Added)
            {
                throw new InvalidOperationException("Entity already existed and state cannnot be changed");
            }

            var dbEntryProperty = entry.Property(name);
            var guid = (Guid)dbEntryProperty.CurrentValue;
            if (guid == default)
            {
                guid = SequentialGuid.Create();
            }

           return guid;
        }
    }
}
