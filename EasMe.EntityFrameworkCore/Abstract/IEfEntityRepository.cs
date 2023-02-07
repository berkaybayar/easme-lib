using System.Linq.Expressions;

namespace EasMe.EntityFrameworkCore.Abstract
{
    public interface IEfEntityRepository<T>
    where T : class, IEfEntity, new()
    {
        public IQueryable<T> Get(Expression<Func<T, bool>>? filter = null);
        public List<T> GetList(Expression<Func<T, bool>>? filter = null);
        public bool Any(Expression<Func<T, bool>> filter);
        public bool Any();
        public T? GetFirstOrDefault(Expression<Func<T, bool>> filter);
        public T? GetFirst(Expression<Func<T, bool>> filter);
        public T GetSingle(Expression<Func<T, bool>> filter);
        public T? GetSingleOrDefault(Expression<Func<T, bool>> filter);
        public T GetSingle();
        public T? GetFirst();
        public bool Add(T entity);
        public int AddRange(IEnumerable<T> entities);
        public bool Update(T entity);
        public bool Update(T entity, Action<T> updateAction);
        public int UpdateRange(IEnumerable<T> entities);
        public int UpdateWhere(Expression<Func<T, bool>> filter, Action<T> updateAction);
        public int DeleteWhere(Expression<Func<T, bool>> filter);
        public bool UpdateWhereSingle(Expression<Func<T, bool>> filter, Action<T> updateAction);
        public bool DeleteWhereSingle(Expression<Func<T, bool>> filter);
        public bool Delete(T entity);
        public bool Delete(int id);
        public int DeleteRange(IEnumerable<T> entities);
        public T? Find(int id);
        public int Count(Expression<Func<T, bool>> filter);
        public int Count();

    }
}
