using Raven.Client.Documents.Session;
using Volo.Abp.Data;
using Volo.Abp.RavenDB.Volo.Abp.RavenDB;

namespace Volo.Abp.RavenDB.Tests.Volo.Abp.TestApp.RavenDb
{
    [ConnectionStringName("TestApp")]
    public interface ITestAppRavenDbContext : IAbpRavenDbContext
    {
        IAsyncDocumentSession AsyncSession { get; }
    }
}