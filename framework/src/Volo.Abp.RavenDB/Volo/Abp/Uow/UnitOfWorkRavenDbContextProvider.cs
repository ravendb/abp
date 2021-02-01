using System;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents.Session;
using Volo.Abp.Data;
using Volo.Abp.RavenDB.Volo.Abp.RavenDB;
using Volo.Abp.Uow;

namespace Volo.Abp.RavenDB.Volo.Abp.Uow
{
    public class UnitOfWorkRavenDbContextProvider<TRavenDbContext> : IRavenDbContextProvider<TRavenDbContext>
        where TRavenDbContext : IAbpRavenDbContext
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IConnectionStringResolver _connectionStringResolver;

        public UnitOfWorkRavenDbContextProvider(
            IUnitOfWorkManager unitOfWorkManager,
            IConnectionStringResolver connectionStringResolver)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _connectionStringResolver = connectionStringResolver;
        }

        public TRavenDbContext GetDbContext()
        {
            IUnitOfWork unitOfWork;
            try
            {
                unitOfWork = _unitOfWorkManager.Current;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            
            if (unitOfWork == null)
            {
                throw new AbpException($"A {nameof(IAsyncDocumentSession)} instance can only be created inside a unit of work!");
            }            

            var connectionString = _connectionStringResolver.Resolve<TRavenDbContext>();
            var dbContextKey = $"{typeof(TRavenDbContext).FullName}_{connectionString}";
                       
            var databaseApi = unitOfWork.GetOrAddDatabaseApi(
                dbContextKey,
                () =>
                {
                    var dbContext = unitOfWork.ServiceProvider.GetRequiredService<TRavenDbContext>();

                    var session = RavenFactory.Store.OpenAsyncSession();

                    //dbContext.ToAbpMongoDbContext().InitializeDatabase(database);

                    return new RavenDbDatabaseApi<TRavenDbContext>(dbContext);
                });

            return ((RavenDbDatabaseApi<TRavenDbContext>)databaseApi).DbContext;
        }
    }
}
