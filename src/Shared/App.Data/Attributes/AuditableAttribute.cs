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
    public class AuditableAttribute : PreSaveActionAttribute
    {
        public override void PerformAction(DbEntityEntry entry, string name)
        {
            var dbEntryProperty = entry.Property(name);
            if (entry.State == EntityState.Added)
            {
                var date = (DateTime?)dbEntryProperty.CurrentValue;
                if (!date.HasValue || date == default)
                {
                    dbEntryProperty.CurrentValue = (DateTime?)DateTime.Now;
                }
                return;
            }

            if (entry.State == EntityState.Modified)
            {
                if (dbEntryProperty.Name == "ModifiedOn")
                {
                    var currentVal = (DateTime?)dbEntryProperty.CurrentValue;
                    var oldVal = (DateTime?)dbEntryProperty.OriginalValue;
                    if (oldVal == currentVal || currentVal == default)
                    {
                        dbEntryProperty.CurrentValue = (DateTime?)DateTime.Now;
                    }
                }
                return;
            }
        }
    }
}
