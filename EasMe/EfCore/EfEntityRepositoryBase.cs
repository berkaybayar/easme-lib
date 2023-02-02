using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.EFCore
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEfEntityRepository<TEntity>
        where TEntity : class, IEfEntity, new()
        where TContext : DbContext, new()
    {
        public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> filter)
        {
            using var context = new TContext();
            return filter == null
                ? context.Set<TEntity>()
                : context.Set<TEntity>().Where(filter);
        }

        public TEntity? GetFirstOrDefault(Expression<Func<TEntity, bool>> filter)
        {
            using var context = new TContext();
            return context.Set<TEntity>().FirstOrDefault(filter);
        }
		public TEntity? GetFirst()
		{
			using var context = new TContext();
			return context.Set<TEntity>().First();
		}

		public TEntity GetSingle()
		{
			using var context = new TContext();
			return context.Set<TEntity>().Single();
		}

		public TEntity? GetFirst(Expression<Func<TEntity, bool>> filter)
        {
            using var context = new TContext();
            return context.Set<TEntity>().First(filter);
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> filter)
        {
            using var context = new TContext();
            return context.Set<TEntity>().Single(filter);
        }

        public TEntity? GetSingleOrDefault(Expression<Func<TEntity, bool>> filter)
        {
            using var context = new TContext();
            return context.Set<TEntity>().SingleOrDefault(filter);
        }

        public bool Add(TEntity entity)
        {
            using var context = new TContext();
            var addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Added;
            return context.SaveChanges() == 1;
        }

        public int AddRange(IEnumerable<TEntity> entities)
        {
            using var context = new TContext();
            var addedEntity = context.Entry(entities);
            addedEntity.State = EntityState.Added;
            return context.SaveChanges();
        }

        public bool Update(TEntity entity)
        {
            using var context = new TContext();
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            return context.SaveChanges() == 1;
        }

        public int UpdateRange(IEnumerable<TEntity> entities)
        {
            using var context = new TContext();
            var addedEntity = context.Entry(entities);
            addedEntity.State = EntityState.Modified;
            return context.SaveChanges();
        }

        public bool Delete(TEntity entity)
        {
            using var context = new TContext();
            var deletedEntity = context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            return context.SaveChanges() == 1;
        }

        public int DeleteRange(IEnumerable<TEntity> entities)
        {
            using var context = new TContext();
            var addedEntity = context.Entry(entities);
            addedEntity.State = EntityState.Deleted;
            return context.SaveChanges();
        }

        public int UpdateWhere(Expression<Func<TEntity, bool>> filter, Action<TEntity> updateAction)
        {
            using var context = new TContext();
            var entities = context.Set<TEntity>().Where(filter).ToList();
            if (entities.Count == 0) return 0;
            foreach (var entry in entities)
            {
                updateAction(entry);
            }
            var updatedEntities = context.Entry(entities);
            updatedEntities.State = EntityState.Modified;
            return context.SaveChanges();
        }

        public bool UpdateWhereSingle(Expression<Func<TEntity, bool>> filter, Action<TEntity> updateAction)
        {
            using var context = new TContext();
            var entity = context.Set<TEntity>().Where(filter).SingleOrDefault();
            if (entity is null) return false;
            updateAction(entity);
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            return context.SaveChanges() == 1;
        }

		public bool Any(Expression<Func<TEntity, bool>> filter)
		{
            using var context = new TContext();
            return context.Set<TEntity>().Any(filter);
		}

		public bool Any()
		{
			using var context = new TContext();
			return context.Set<TEntity>().Any();
		}

		public TEntity? Find(int id)
		{
			using var context = new TContext();
			return context.Set<TEntity>().Find(id);
		}

		public bool Update(TEntity entity, Action<TEntity> updateAction)
		{
			using var context = new TContext();
			updateAction(entity);
			var updatedEntities = context.Entry(entity);
			updatedEntities.State = EntityState.Modified;
			return context.SaveChanges() == 1;
		}

		public int Count(Expression<Func<TEntity, bool>> filter)
		{
			using var context = new TContext();
			return context.Set<TEntity>().Count(filter);
		}

		public int Count()
		{
			using var context = new TContext();
			return context.Set<TEntity>().Count();
		}

		public bool Delete(int id)
		{
            var data = Find(id);
            if (data is null) return false;
            return Delete(data);
		}
	}
}
