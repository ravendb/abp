using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.DependencyInjection;
using Volo.Abp.RavenDB.Volo.Abp.RavenDB.DependencyInjection;

namespace Volo.Abp.RavenDB.Microsoft.Extensions.DependencyInjection
{
    public class AbpRavenDbContextRegistrationOptions : AbpCommonDbContextRegistrationOptions, IAbpRavenDbContextRegistrationOptionsBuilder
    {
        public AbpRavenDbContextRegistrationOptions(Type originalDbContextType, IServiceCollection services)
            : base(originalDbContextType, services)
        {
        }
    }
}
