using Volo.Abp.Modularity;
using Raven.TestDriver;
using Volo.Abp.Data;
using Raven.Embedded;
using System.Linq;
using System;
using Volo.Abp.RavenDB.Tests.Volo.Abp.TestApp.RavenDb;
using Volo.Abp.TestApp;
using Volo.Abp.RavenDB.Volo.Abp.RavenDB;
using Microsoft.Extensions.DependencyInjection;

namespace Volo.Abp.RavenDB.Tests.Volo.RavenDB
{
    [DependsOn(
        typeof(AbpRavenDbModule),
        typeof(TestAppModule)
        )]
    public class AbpRavenDbTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            EmbeddedServer.Instance.StartServer(new ServerOptions
            {
                FrameworkVersion = null
            });

            using (var store = EmbeddedServer.Instance.GetDocumentStore("Embedded"))
            {
                Configure<AbpDbConnectionOptions>(options =>
                {
                    options.ConnectionStrings.Default = store.Urls.First() + "Db_" + Guid.NewGuid().ToString("N"); ;
                });                

                using (var session = store.OpenSession())
                {                    
                }
            }

            /*context.Services.AddRavenDbContext<TestAppRavenDbContext>(options =>
            {
                options.AddDefaultRepositories<ITestAppRavenDbContext>();
                options.AddRepository<City, CityRepository>();
            });*/

            /*using (var store = GetDocumentStore())
            { 
                
               
            }*/

            /*var connectionString = MongoDbFixture.ConnectionString.EnsureEndsWith('/')  +
                                   "Db_" +
                                   Guid.NewGuid().ToString("N");

            Configure<AbpDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = connectionString;
            });

            context.Services.AddMongoDbContext<TestAppMongoDbContext>(options =>
            {
                options.AddDefaultRepositories<ITestAppMongoDbContext>();
                options.AddRepository<City, CityRepository>();
            });

            Configure<AbpUnitOfWorkDefaultOptions>(options =>
            {
                options.TransactionBehavior = UnitOfWorkTransactionBehavior.Disabled;
            });*/
        }
    }
}