using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.RavenDB.Microsoft.Extensions.DependencyInjection;
using Volo.Abp.RavenDB.Volo.Abp.Domain.Repositories.RavenDB;
using Volo.Abp.Reflection;

namespace Volo.Abp.RavenDB.Volo.Abp.RavenDB.DependencyInjection
{
    public class RavenDbRepositoryRegistrar : RepositoryRegistrarBase<AbpRavenDbContextRegistrationOptions>
    {
        public RavenDbRepositoryRegistrar(AbpRavenDbContextRegistrationOptions options)
            : base(options)
        {

        }

        protected override IEnumerable<Type> GetEntityTypes(Type dbContextType)
        {
            /* return
                 from property in dbContextType.GetTypeInfo().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                 where
                     ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(IMongoCollection<>)) &&
                     typeof(IEntity).IsAssignableFrom(property.PropertyType.GenericTypeArguments[0])
                 select property.PropertyType.GenericTypeArguments[0];*/

            return null;
        }

        protected override Type GetRepositoryType(Type dbContextType, Type entityType)
        {
            return typeof(RavenDbRepository<,>).MakeGenericType(dbContextType, entityType);
        }

        protected override Type GetRepositoryType(Type dbContextType, Type entityType, Type primaryKeyType)
        {
            return typeof(RavenDbRepository<,,>).MakeGenericType(dbContextType, entityType, primaryKeyType);
        }
    }
}
