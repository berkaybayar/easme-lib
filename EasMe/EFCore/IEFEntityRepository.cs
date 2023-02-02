using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.EFCore
{
    public interface IEfEntityRepository<T>
    where T : class, IEfEntity, new()
    {
        public IQueryable<T> GetWhere(Expression<Func<T, bool>> filter);
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
        public int UpdateWhere(Expression<Func<T, bool>> wherefilter, Action<T> updateAction);
        public bool UpdateWhereSingle(Expression<Func<T, bool>> filter, Action<T> updateAction);
		public bool Delete(T entity);
        public bool Delete(int id);
        public int DeleteRange(IEnumerable<T> entities);
        public T? Find(int id);
        public int Count(Expression<Func<T, bool>> filter);
        public int Count();

    }
}
