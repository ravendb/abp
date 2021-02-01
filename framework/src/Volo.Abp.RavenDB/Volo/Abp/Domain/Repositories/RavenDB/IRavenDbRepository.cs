using Raven.Client.Documents.Session;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Volo.Abp.RavenDB.Volo.Abp.Domain.Repositories.RavenDB
{
    public interface IRavenDbRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        IAsyncDocumentSession Session { get; }        
    }

    public interface IRavenDbRepository<TEntity, TKey> : IRavenDbRepository<TEntity>, IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {

    }
}
