using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.RavenDB.Volo.Abp.RavenDB;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.RavenDB.Volo.Abp.RavenDB.DependencyInjection;

namespace Volo.Abp.RavenDB.Microsoft.Extensions.DependencyInjection
{
    public static class AbpRavenDbServiceCollectionExtensions
    {
        public static IServiceCollection AddRavenDbAsyncSession(
            this IServiceCollection services,
            RavenSettings settings)
        {
            var store = new DocumentStore()
            {
                Database = settings.DatabaseName,
                Urls = settings.Urls,
                Certificate = string.IsNullOrEmpty(settings.CertFilePath) ? null : new X509Certificate2(settings.CertFilePath, settings.CertPassword)
            };
            store.Initialize();
            services.AddSingleton<IDocumentStore>(store);
            return services.AddScoped(sp => sp.GetRequiredService<IDocumentStore>().OpenAsyncSession());
        }

        public static IServiceCollection AddRavenbDbContext<TRavenDbContext>(this IServiceCollection services, Action<IAbpRavenDbContextRegistrationOptionsBuilder> optionsBuilder = null) //Created overload instead of default parameter
            where TRavenDbContext : AbpRavenDbContext
        {
            var options = new AbpRavenDbContextRegistrationOptions(typeof(TRavenDbContext), services);
            optionsBuilder?.Invoke(options);

            foreach (var dbContextType in options.ReplacedDbContextTypes)
            {
                services.Replace(ServiceDescriptor.Transient(dbContextType, typeof(TRavenDbContext)));
            }

            new RavenDbRepositoryRegistrar(options).AddRepositories();

            return services;
        }        
    }
}
