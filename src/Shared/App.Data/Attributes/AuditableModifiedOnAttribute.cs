using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Attributes
{
    public class AuditableModifiedOnAttribute : PreSaveActionAttribute
    {
        public override bool CanPerformAction(DbEntityEntry entry, string name)
        {
            var propertyType = entry.Property(name).GetType();

            return propertyType == typeof(DateTime?);
        }

        public override object PerformAction(DbEntityEntry entry, string name)
        {
            var dbEntryProperty = entry.Property(name);
            var oldVal = (DateTime?)dbEntryProperty.OriginalValue;


            if (entry.State == EntityState.Added)
            {
                return (!oldVal.HasValue || oldVal == default) ? DateTime.UtcNow as DateTime? : oldVal;
            }

            if (entry.State == EntityState.Modified)
            {
                var currentVal = (DateTime?)dbEntryProperty.CurrentValue;

                return (oldVal == currentVal || currentVal == default) ? DateTime.UtcNow as DateTime? : currentVal;
            }

            throw new InvalidOperationException("cannot changed the entity for another state");
        }
    }
}
