using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.RavenDB.Volo.Abp.RavenDB;

namespace Volo.Abp.RavenDB.Volo.Abp.Domain.Repositories.RavenDB
{
    public class RavenDbRepository<TRavenDbContext, TEntity> : RepositoryBase<TEntity>
        where TRavenDbContext : IAbpRavenDbContext
        where TEntity : class, IEntity
    {
        public virtual IAsyncDocumentSession Session => DbContext.AsyncSession;

        public virtual TRavenDbContext DbContext => DbContextProvider.GetDbContext();        

        protected IRavenDbContextProvider<TRavenDbContext> DbContextProvider { get; }

        public RavenDbRepository(IRavenDbContextProvider<TRavenDbContext> dbContextProvider)
        {
            DbContextProvider = dbContextProvider;            
        }

        public override async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var entities = await GetQueryable()
                .Where(predicate)
                .ToListAsync(GetCancellationToken(cancellationToken));

            foreach (var entity in entities)
            {
                Session.Delete(entity);
            }

            if (autoSave)
            {
                await Session.SaveChangesAsync(GetCancellationToken(cancellationToken));
            }
        }

        public override async Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            Session.Delete(entity);

            if (autoSave)
            {
                await Session.SaveChangesAsync(GetCancellationToken(cancellationToken));
            }
        }

        public override async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return await GetQueryable().Where(predicate).SingleOrDefaultAsync(GetCancellationToken(cancellationToken));
        }

        public override async Task<long> GetCountAsync(CancellationToken cancellationToken = default)
        {
            return await GetQueryable().CountAsync(GetCancellationToken(cancellationToken));
        }

        public override async Task<List<TEntity>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            return await GetQueryable().ToListAsync(GetCancellationToken(cancellationToken));
        }

        public override async Task<List<TEntity>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            return await Session.Query<TEntity>()
                .OrderBy(sorting).PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public override async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await Session.StoreAsync(entity, GetCancellationToken(cancellationToken));

            if (autoSave)
            {
                await Session.SaveChangesAsync(GetCancellationToken(cancellationToken));
            }

            return entity;
        }

        public override async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await Session.StoreAsync(entity, GetCancellationToken(cancellationToken));            

            if (autoSave)
            {
                await Session.SaveChangesAsync(GetCancellationToken(cancellationToken));
            }

            return entity;
        }

        protected override IQueryable<TEntity> GetQueryable()
        {
            return Session.Query<TEntity>();
        }

        public override Task<IQueryable<TEntity>> GetQueryableAsync()
        {
            throw new NotImplementedException();
        }
    }

    public class RavenDbRepository<TRavenDbContext, TEntity, TKey>
        : RavenDbRepository<TRavenDbContext, TEntity>,
        IRavenDbRepository<TEntity>
        where TRavenDbContext : IAbpRavenDbContext
        where TEntity : class, IEntity<TKey>
    {
        public RavenDbRepository(IRavenDbContextProvider<TRavenDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<TEntity> GetAsync(
            TKey id,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            var entity = await FindAsync(id, includeDetails, cancellationToken);

            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }

            return entity;
        }

        public virtual async Task<TEntity> FindAsync(
            TKey id,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            //return await Session.Query<TEntity>().SingleOrDefault(x => x.Id == id, GetCancellationToken(cancellationToken));
            return null;
        }

        public virtual Task DeleteAsync(
            TKey id,
            bool autoSave = false,
            CancellationToken cancellationToken = default)
        {
            return DeleteAsync(x => x.Id.Equals(id), autoSave, cancellationToken);
        }

        /*protected override FilterDefinition<TEntity> CreateEntityFilter(TEntity entity, bool withConcurrencyStamp = false, string concurrencyStamp = null)
        {
            return RepositoryFilterer.CreateEntityFilter(entity, withConcurrencyStamp, concurrencyStamp);
        }*/
    }
}
