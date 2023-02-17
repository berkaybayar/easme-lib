using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EasMe.EntityFrameworkCore.V2
{
    public abstract class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity> 
        where TEntity : class, IEntity
        where TContext : DbContext, new()
    {
        private readonly TContext _dbContext;

        private DbSet<TEntity> Table { get; }
        
        protected GenericRepository(TContext context)
        {
            this._dbContext = context;
            this.Table = context.Set<TEntity>();
        }

        public virtual TEntity? GetFirstOrDefault(
            Expression<Func<TEntity, bool>>? filter = null,
            params string[] includeProperties)
        {
            var query = Table.AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>>? filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, 
            params string[] includeProperties)
        {
            var query = Table.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return query.ToList();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            params string[] includeProperties)
        {
            return Get(filter, null, includeProperties);
        }
        public virtual IEnumerable<TEntity> GetPaging(
            int page,
            int pageSize = 15,
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            params string[] includeProperties)
        {
            var query = Table.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            var skipItemIndex = (page - 1) * pageSize;
            return query
                .Skip(skipItemIndex)
                .Take(pageSize)
                .ToList();
        }
        public virtual TEntity? GetById(object id)
        {
            return Table.Find(id);
        }
        public virtual TEntity? GetById(params object[] id)
        {
            return Table.Find(id);
        }
        public virtual void Insert(TEntity entity)
        {
            Table.Add(entity);
        }
        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            Table.AddRange(entities);
        }
        public virtual void Delete(object id)
        {
            var entityToDelete = Table.Find(id);
            if (entityToDelete != null)
            {
                Delete(entityToDelete);
            }
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                Table.Attach(entityToDelete);
            }
            Table.Remove(entityToDelete);
        }
        public virtual void Update(TEntity entityToUpdate)
        {
            Table.Attach(entityToUpdate);
            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            var enumerable = entities.ToList();
            Table.AttachRange(enumerable);
            _dbContext.Entry(enumerable).State = EntityState.Modified;
        }
        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            var enumerable = entities.ToList();
            if (_dbContext.Entry(enumerable).State == EntityState.Detached)
            {
                Table.AttachRange(enumerable);
            }
            Table.RemoveRange(enumerable);
        }
        public bool Any(Expression<Func<TEntity, bool>>? filter = null)
        {
            if (filter == null) return Table.Any();
            return Table.Any(filter);
        }
        public int Count(Expression<Func<TEntity, bool>>? filter = null)
        {
            if(filter == null) return Table.Count();
            return Table.Count(filter);
        }
    }
}