using Raven.Client.Documents.Session;
using Volo.Abp.Data;

namespace Volo.Abp.RavenDB.Tests.Volo.Abp.TestApp.RavenDb
{
    [ConnectionStringName("TestApp")]
    public class TestAppRavenDbContext : ITestAppRavenDbContext
    {
        public IAsyncDocumentSession AsyncSession { get; }
    }
}