using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace EasMe.EntityFrameworkCore;

public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
    where TEntity : class, IEntity
    where TContext : DbContext, new() {
    private readonly TContext _dbContext;

    public GenericRepository(TContext context) {
        _dbContext = context;
        Table = context.Set<TEntity>();
    }

    private DbSet<TEntity> Table { get; }

    public virtual TEntity? GetFirstOrDefault(
        Expression<Func<TEntity, bool>>? filter = null,
        params string[] includeProperties) {
        var query = Table.AsQueryable();
        if (filter != null) query = query.Where(filter);
        foreach (var includeProperty in includeProperties) query = query.Include(includeProperty);
        return query.FirstOrDefault();
    }

    public virtual TEntity? GetFirstOrDefaultOrdered(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params string[] includeProperties) {
        var query = Table.AsQueryable();
        if (filter != null) query = query.Where(filter);
        foreach (var includeProperty in includeProperties) query = query.Include(includeProperty);
        if (orderBy != null) query = orderBy(query);
        return query.FirstOrDefault();
    }

    public virtual TEntity? GetLastOrDefaultOrdered(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params string[] includeProperties) {
        var query = Table.AsQueryable();
        if (filter != null) query = query.Where(filter);
        foreach (var includeProperty in includeProperties) query = query.Include(includeProperty);
        if (orderBy != null) query = orderBy(query);
        return query.LastOrDefault();
    }

    public virtual IQueryable<TEntity> GetOrdered(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params string[] includeExpressions) {
        var query = Table.AsQueryable();

        if (filter != null) query = query.Where(filter);

        foreach (var includeProperty in includeExpressions) query = query.Include(includeProperty);

        if (orderBy != null) query = orderBy(query);
        return query;
    }

    public virtual IQueryable<TResult> GetSelect<TResult>(
        Expression<Func<TEntity, TResult>> select,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params string[] includeExpressions) {
        var query = Table.AsQueryable();
        foreach (var includeProperty in includeExpressions) query = query.Include(includeProperty);

        if (filter != null) query = query.Where(filter);

        if (orderBy != null) query = orderBy(query);
        return query.Select(select);
    }

    public TResult? GetFirstOrDefaultSelect<TResult>(Expression<Func<TEntity, TResult>> select,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params string[] includeExpressions) {
        var query = Table.AsQueryable();
        foreach (var includeProperty in includeExpressions) query = query.Include(includeProperty);

        if (filter != null) query = query.Where(filter);

        if (orderBy != null) query = orderBy(query);
        return query.Select(select).FirstOrDefault();
    }

    public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null,
        params string[] includeExpressions) {
        return GetOrdered(filter, null, includeExpressions);
    }

    public virtual IQueryable<TEntity> GetPaging(int page, int pageSize = 15,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params string[] includeExpressions) {
        var query = Table.AsQueryable();

        if (filter != null) query = query.Where(filter);

        foreach (var func in includeExpressions) query = query.Include(func);


        if (orderBy != null) query = orderBy(query);
        var skipItemIndex = (page - 1) * pageSize;
        return query
            .Skip(skipItemIndex)
            .Take(pageSize);
    }


    public virtual TEntity? GetById(object id) {
        return Table.Find(id);
    }

    public virtual TEntity? GetById(params object[] id) {
        return Table.Find(id);
    }

    public virtual void Insert(TEntity entity) {
        Table.Add(entity);
    }

    public virtual void InsertRange(IEnumerable<TEntity> entities) {
        Table.AddRange(entities);
    }

    public virtual void Delete(object id) {
        var entityToDelete = Table.Find(id);
        if (entityToDelete != null) Delete(entityToDelete);
    }

    public virtual void Delete(TEntity entityToDelete) {
        if (_dbContext.Entry(entityToDelete).State == EntityState.Detached) Table.Attach(entityToDelete);
        Table.Remove(entityToDelete);
    }

    public virtual void Update(TEntity entityToUpdate) {
        Table.Attach(entityToUpdate);
        _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
    }

    public virtual void UpdateRange(IEnumerable<TEntity> entities) {
        var enumerable = entities.ToList();
        Table.AttachRange(enumerable);
        foreach (var entity in enumerable) _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public virtual void DeleteRange(IEnumerable<TEntity> entities) {
        var enumerable = entities.ToList();
        if (_dbContext.Entry(enumerable).State == EntityState.Detached) Table.AttachRange(enumerable);
        Table.RemoveRange(enumerable);
    }

    public virtual bool Any(Expression<Func<TEntity, bool>>? filter = null) {
        if (filter == null) return Table.Any();
        return Table.Any(filter);
    }

    public virtual int Count(Expression<Func<TEntity, bool>>? filter = null) {
        if (filter == null) return Table.Count();
        return Table.Count(filter);
    }
}