using App.Data.Attributes;
using App.Data.Helpers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace App.Data.Contexts
{
    public class ReadWriteAppContext : ApplicationDbContext
    {
        public ReadWriteAppContext() : base("ReadWriteAppContext")
        {
        }

        public ReadWriteAppContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {
        }

        public override async Task<int> SaveChangesAsync()
        {
            //PreSaveChanges();
            return await base.SaveChangesAsync();
        }

        public override int SaveChanges()
        {
            //PreSaveChanges();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            //PreSaveChanges();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void PreSaveChanges()
        {
           if (ChangeTracker.HasChanges())
           {
                foreach (var entityEntry in ChangeTracker.Entries())
                {
                    PreSaveActionHelper.ApplyChanges(entityEntry);
                }
            }
        }
    }
}
