using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Data.Contexts
{
    public class ReadAppContext : ApplicationDbContext
    {
        public ReadAppContext() : base("ReadAppContext")
        {
            Database.Log = value => Debug.WriteLine(value);
            Configuration.AutoDetectChangesEnabled = false;
        }

        public ReadAppContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {
            Database.Log = value => Debug.WriteLine(value);
        }

        public override int SaveChanges()
        {
            throw new NotImplementedException("cannot save changes on this context") ;
        }

        public override Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException("cannot save changes on this context");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException("cannot save changes on this context");
        }
    }
}
