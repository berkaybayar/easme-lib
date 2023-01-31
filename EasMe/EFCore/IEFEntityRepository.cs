using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.EFCore
{
    public interface IEFEntityRepository<T>
    where T : class, IEFEntity, new()
    {
        public List<T> GetList(Expression<Func<T, bool>>? filter = null);
        public T? GetFirstOrDefault(Expression<Func<T, bool>> filter);
        public T? GetFirst(Expression<Func<T, bool>> filter);
        public T? GetSingle(Expression<Func<T, bool>> filter);
        public T? GetSingleOrDefault(Expression<Func<T, bool>> filter);
        public bool Add(T entity);
        public int AddRange(IEnumerable<T> entities);
        public bool Update(T entity);
        public int UpdateRange(IEnumerable<T> entities);
        public int Update(Expression<Func<T, bool>> filter, Action<T> updateAction);
        public bool UpdateSingleOrDefault(Expression<Func<T, bool>> filter, Action<T> updateAction);
        public bool Delete(T entity);
        public int DeleteRange(IEnumerable<T> entities);
    }
}
