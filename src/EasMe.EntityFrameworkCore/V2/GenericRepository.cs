using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EasMe.EntityFrameworkCore.V2
{
    public class GenericRepository<TEntity, TContext>
        where TEntity : class
        where TContext : DbContext, new()
    {
        private readonly TContext _dbContext;

        public DbSet<TEntity> Table { get; }
        
        public GenericRepository(TContext context)
        {
            this._dbContext = context;
            this.Table = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
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
    }
}