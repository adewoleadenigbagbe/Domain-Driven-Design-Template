using App.Data.Attributes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Helpers
{
    public class PreSaveActionHelper
    {
        private static ConcurrentDictionary<Type, IEnumerable<EntityPropertyAttribute>>  entityattributeCache = new ConcurrentDictionary<Type, IEnumerable<EntityPropertyAttribute>>();

        public static void AddEntityType(IEnumerable<DbEntityEntry> entityEntries)
        {
            foreach (var entityEntry in entityEntries)
            {
                var entityType = entityEntry.Entity.GetType();
                var propertyAttributes = GetEntityPropertyAttribute(entityType);

                entityattributeCache.GetOrAdd(entityType, propertyAttributes);
            }
        }


        private static IEnumerable<EntityPropertyAttribute> GetEntityPropertyAttribute(Type type)
        {
            foreach (var propertyInfo in type.GetProperties())
            {
                var customAttributes = propertyInfo.GetCustomAttributes()
                    .Where(x => x is PreSaveActionAttribute)
                    .Select(x => x as PreSaveActionAttribute);

                var entityPropertyAttribute = new EntityPropertyAttribute
                {
                    PropertyInfo = propertyInfo,
                    PreSaveActionAttributes = customAttributes
                };

                yield return entityPropertyAttribute;
            }
        }
    }

    public class EntityPropertyAttribute
    {
        public PropertyInfo PropertyInfo { get; set; }

        public IEnumerable<PreSaveActionAttribute> PreSaveActionAttributes { get; set; }
    }
}
