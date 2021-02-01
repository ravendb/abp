using Volo.Abp.Uow;

namespace Volo.Abp.RavenDB.Volo.Abp.Uow
{
    public class RavenDbDatabaseApi<TRavenDbContext> : IDatabaseApi
    {
        public TRavenDbContext DbContext { get; }

        public RavenDbDatabaseApi(TRavenDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
