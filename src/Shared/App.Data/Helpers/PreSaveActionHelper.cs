using App.Data.Attributes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Helpers
{
    public static class PreSaveActionHelper
    {
        private static ConcurrentDictionary<Type, IEnumerable<EntityPropertyAttribute>>  entityattributeCache = new ConcurrentDictionary<Type, IEnumerable<EntityPropertyAttribute>>();

        public static IEnumerable<EntityPropertyAttribute> GetOrAddToCache(DbEntityEntry entityEntry)
        {
            var entityType = entityEntry.Entity.GetType();
            if (!entityattributeCache.TryGetValue(entityType, out var propertyAttributes))
            {
                propertyAttributes = GetEntityPropertyAttribute(entityEntry);
            }

            return entityattributeCache.GetOrAdd(entityType, propertyAttributes);
        }

        public static void ApplyChanges(DbEntityEntry entityEntry)
        {
            var entityPropertyAttributes = GetOrAddToCache(entityEntry);

            foreach (var entityPropertyAttribute in entityPropertyAttributes)
            {
                foreach (var presaveAttribute in entityPropertyAttribute.PreSaveActionAttributes)
                {
                    var canPerform = presaveAttribute.CanPerformAction(entityEntry, entityPropertyAttribute.PropertyType);
                    if (!canPerform)
                    {
                        throw new NotSupportedException($"cannot perform this specific attribute action on {entityPropertyAttribute.PropertyName} as {nameof(entityPropertyAttribute.PropertyType)}");
                    }

                    var originalValue = entityEntry.Property(entityPropertyAttribute.PropertyName).OriginalValue;
                    var currentValue = entityPropertyAttribute.Get(entityEntry.Entity);

                    var newVal = presaveAttribute.PerformAction(entityEntry, originalValue, currentValue);

                    entityPropertyAttribute.Set(entityEntry.Entity, newVal);
                }
            }
        }


        private static IEnumerable<EntityPropertyAttribute> GetEntityPropertyAttribute(DbEntityEntry entityEntry)
        {
            var entityType = entityEntry.Entity.GetType();
            foreach (var propertyInfo in entityType.GetProperties())
            {
                var customAttributes = propertyInfo.GetCustomAttributes()
                    .Where(x => x is PreSaveActionAttribute)
                    .Select(x => x as PreSaveActionAttribute);

                var entityPropertyAttribute = new EntityPropertyAttribute
                {
                    PropertyName = propertyInfo.Name,
                    PropertyType = propertyInfo.PropertyType,
                    Get = (entity) => propertyInfo.GetValue(entity),
                    Set = (entity, value) => propertyInfo.SetValue(entity, value),
                    PreSaveActionAttributes = customAttributes
                };

                yield return entityPropertyAttribute;
            }
        }
    }

    public class EntityPropertyAttribute
    {
        public string PropertyName { get; set; }

        public Type PropertyType { get; set; }

        public Func<object,object> Get { get; set; }

        public Action<object,object> Set { get; set; }

        public IEnumerable<PreSaveActionAttribute> PreSaveActionAttributes { get; set; }
    }
}
