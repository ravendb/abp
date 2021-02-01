using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.RavenDB.Volo.Abp.Uow;

namespace Volo.Abp.RavenDB.Volo.Abp.RavenDB
{
    [DependsOn(typeof(AbpDddDomainModule))]
    public class AbpRavenDbModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.TryAddTransient(
                typeof(IRavenDbContextProvider<>),
                typeof(UnitOfWorkRavenDbContextProvider<>)
            );
        }
    }
}
