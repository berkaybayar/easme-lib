using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EasMe.EntityFrameworkCore.V1
{
    /// <summary>
    /// Base EntityFrameworkCore Repository class to implement under Unit of Work pattern
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public class EntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        private readonly TContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        public EntityRepositoryBase(TContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }
        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null)
        {
            return filter == null
                ? _dbSet
                : _dbSet.Where(filter);
        }
        public List<TEntity> GetList(Expression<Func<TEntity, bool>>? filter = null)
        {
            return filter == null
                ? _dbSet.ToList()
                : _dbSet.Where(filter).ToList();
        }
        public TEntity? GetFirstOrDefault(Expression<Func<TEntity, bool>> filter)
        {
            return _dbSet.FirstOrDefault(filter);
        }

        public TEntity? GetFirstOrDefault()
        {
            return _dbSet.FirstOrDefault();
        }

        public TEntity? GetFirst()
        {
            return _dbSet.First();
        }

        public TEntity? GetSingleOrDefault()
        {
            return _dbSet.SingleOrDefault();

        }

        public TEntity GetSingle()
        {
            return _dbSet.Single();
        }

        public TEntity? GetFirst(Expression<Func<TEntity, bool>> filter)
        {
            return _dbSet.First(filter);
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> filter)
        {
            return _dbSet.Single(filter);
        }

        public TEntity? GetSingleOrDefault(Expression<Func<TEntity, bool>> filter)
        {
            return _dbSet.SingleOrDefault(filter);
        }

        public void Add(TEntity entity)
        {
            var addedEntity = _dbContext.Entry(entity);
            addedEntity.State = EntityState.Added;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            var addedEntity = _dbContext.Entry(entities);
            addedEntity.State = EntityState.Added;
        }

        public void Update(TEntity entity)
        {
            var updatedEntity = _dbContext.Entry(entity);
            updatedEntity.State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            var addedEntity = _dbContext.Entry(entities);
            addedEntity.State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            var deletedEntity = _dbContext.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            var addedEntity = _dbContext.Entry(entities);
            addedEntity.State = EntityState.Deleted;
        }

        //public int UpdateWhere(Expression<Func<TEntity, bool>> filter, Action<TEntity> updateAction)
        //{
        //    using var _dbContext = new TContext();
        //    var entities = _dbSet.Where(filter).ToList();
        //    if (entities.Count == 0) return 0;
        //    foreach (var entry in entities)
        //    {
        //        updateAction(entry);
        //    }
        //    var updatedEntities = _dbContext.Entry(entities);
        //    updatedEntities.State = EntityState.Modified;
        //    return _dbContext.SaveChanges();
        //}

        //public bool UpdateWhereSingle(Expression<Func<TEntity, bool>> filter, Action<TEntity> updateAction)
        //{
        //    using var _dbContext = new TContext();
        //    var entity = _dbSet.Where(filter).SingleOrDefault();
        //    if (entity is null) return false;
        //    updateAction(entity);
        //    var updatedEntity = _dbContext.Entry(entity);
        //    updatedEntity.State = EntityState.Modified;
        //    return _dbContext.SaveChanges() == 1;
        //}

        public bool Any(Expression<Func<TEntity, bool>> filter)
        {
            return _dbSet.Any(filter);
        }

        public bool Any()
        {
            return _dbSet.Any();
        }

        public TEntity? Find(int id)
        {
            return _dbSet.Find(id);
        }

        public void Update(TEntity entity, Action<TEntity> updateAction)
        {
            updateAction(entity);
            var updatedEntities = _dbContext.Entry(entity);
            updatedEntities.State = EntityState.Modified;
        }

        public int Count(Expression<Func<TEntity, bool>> filter)
        {
            return _dbSet.Count(filter);
        }

        public int Count()
        {
            return _dbSet.Count();
        }

        //public bool Save()
        //{
        //    return __dbContext.SaveChanges() > 0;
        //}

        //public async Task<bool> SaveAsync()
        //{
        //    return await __dbContext.SaveChangesAsync() > 0;
        //}

        public void Delete(int id)
        {
            var data = Find(id);
            if (data != null)
            {
                _dbContext.Remove(data);

            }
        }

        //public int DeleteWhere(Expression<Func<TEntity, bool>> filter)
        //      {
        //	using var _dbContext = new TContext();
        //	var entities = _dbSet.Where(filter).ToList();
        //	if (entities.Count == 0) return 0;
        //	var updatedEntities = _dbContext.Entry(entities);
        //	updatedEntities.State = EntityState.Deleted;
        //	return _dbContext.SaveChanges();
        //}

        //public bool DeleteWhereSingle(Expression<Func<TEntity, bool>> filter)
        //{
        //	using var _dbContext = new TContext();
        //	var entity = _dbSet.Where(filter).SingleOrDefault();
        //	if (entity is null) return false;
        //	var updatedEntity = _dbContext.Entry(entity);
        //	updatedEntity.State = EntityState.Deleted;
        //	return _dbContext.SaveChanges() == 1;
        //}
    }
}
