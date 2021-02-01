namespace Volo.Abp.RavenDB.Volo.Abp.RavenDB
{
    public interface IRavenDbContextProvider<out TRavenDbContext>
        where TRavenDbContext : IAbpRavenDbContext
    {
        TRavenDbContext GetDbContext();
    }
}
