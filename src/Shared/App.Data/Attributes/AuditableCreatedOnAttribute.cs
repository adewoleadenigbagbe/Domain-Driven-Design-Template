using App.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Attributes
{
    public class AuditableCreatedOnAttribute : PreSaveActionAttribute
    {
        public override bool CanPerformAction(DbEntityEntry entry, string name)
        {
            var propertyType = entry.Property(name).GetType();

            return propertyType == typeof(DateTime?);
        }

        public override object PerformAction(DbEntityEntry entry, string name)
        {
            if (entry.State == EntityState.Added)
            {
                var dbEntryProperty = entry.Property(name);
                var date = (DateTime?)dbEntryProperty.OriginalValue;
                return (!date.HasValue || date == default) ? (DateTime?)DateTime.UtcNow : date;
            }

            throw new InvalidOperationException("cannot changed the entity for another state");
        }
    }
}
