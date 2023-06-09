using System.Linq.Expressions;

namespace EasMe.EntityFrameworkCore.V2;

public interface IGenericRepository<TEntity>
    where TEntity : class, IEntity
{
    TEntity? GetFirstOrDefault(Expression<Func<TEntity, bool>>? filter = null, params string[] includeProperties);

    List<TEntity> ToList()
    {
        return Get().ToList();
    }

    TEntity? GetFirstOrDefaultOrdered(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params string[] includeProperties);

    TEntity? GetLastOrDefaultOrdered(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params string[] includeProperties);

    IQueryable<TEntity> GetOrdered(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params string[] includeExpressions);

    IQueryable<TEntity> Get(
        Expression<Func<TEntity, bool>>? filter = null,
        params string[] includeExpressions);

    IQueryable<TEntity> GetPaging(
        int page,
        int pageSize = 15,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params string[] includeExpressions);

    IQueryable<TResult> GetSelect<TResult>(
        Expression<Func<TEntity, TResult>> select,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params string[] includeExpressions);

    TResult? GetFirstOrDefaultSelect<TResult>(
        Expression<Func<TEntity, TResult>> select,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params string[] includeExpressions);

    TEntity? GetById(object id);
    TEntity? GetById(params object[] id);
    void Insert(TEntity entity);
    void InsertRange(IEnumerable<TEntity> entities);
    void Delete(object id);
    void Delete(TEntity entityToDelete);
    void Update(TEntity entityToUpdate);
    void UpdateRange(IEnumerable<TEntity> entities);
    void DeleteRange(IEnumerable<TEntity> entities);
    bool Any(Expression<Func<TEntity, bool>>? filter = null);
    int Count(Expression<Func<TEntity, bool>>? filter = null);
}