using Raven.Client.Documents.Session;

namespace Volo.Abp.RavenDB.Volo.Abp.RavenDB
{
    public interface IAbpRavenDbContext
    {
        IAsyncDocumentSession AsyncSession { get; }
    }
}
