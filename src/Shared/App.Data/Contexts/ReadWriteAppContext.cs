using App.Data.Attributes;
using System.Data.Common;
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
            PreSaveChanges();
            return await base.SaveChangesAsync();
        }

        public override int SaveChanges()
        {
            PreSaveChanges();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            PreSaveChanges();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void PreSaveChanges()
        {
           if (ChangeTracker.HasChanges())
            {
                foreach(var entry in ChangeTracker.Entries())
                {
                    entry.State =  System.Data.Entity.EntityState.Modified;
                    var propertyInfos = entry.Entity.GetType().GetProperties();
                    foreach (var propertyInfo in propertyInfos)
                    {
                        var customAttributes = propertyInfo.GetCustomAttributes();
                        foreach (var customAttribute in customAttributes)
                        {
                            if (customAttribute is PreSaveActionAttribute)
                            {
                                var presaveActionAttribute = customAttribute as PreSaveActionAttribute;
                                presaveActionAttribute.PerformAction(entry, propertyInfo.Name);
                            }
                        }
                    }
                }
            }
        }

    }
}
