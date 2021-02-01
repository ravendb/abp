using Raven.Client.Documents.Session;

namespace Volo.Abp.RavenDB.Volo.Abp.RavenDB
{
    public class AbpRavenDbContext : IAbpRavenDbContext
    {
        public IAsyncDocumentSession AsyncSession { get; private set; }

        public virtual void InitializeDatabase(IAsyncDocumentSession asyncSession)
        {
            AsyncSession = asyncSession;            
        }
    }
}
