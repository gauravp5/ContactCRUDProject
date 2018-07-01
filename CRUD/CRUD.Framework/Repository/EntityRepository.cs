using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CRUD.Framework.Repository
{
    public interface IEntityRepository<TEntity>
        where TEntity : class, IObjectState, new()
    {
        IQueryable<TEntity> All { get; }

        IQueryable<TEntity> AllWithNoTracking { get; }

        IQueryable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);

        void Attach(TEntity entity);

        void AttachRange(IEnumerable<TEntity> entities);

        void Create(TEntity entity);

        void CreateRange(IEnumerable<TEntity> entities);

        void DeleteRange();

        void Delete(TEntity entity);

        void Update(TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entities);

        int Save();

        Task<int> SaveAsync();
    }

    public class EntityRepository<TEntity> : IEntityRepository<TEntity>
        where TEntity : class, IObjectState, new()
    {
        #region Local Members

        protected DbContext Context;

        #endregion Local Members

        public EntityRepository(DbContext context)
        {
            if (context != null)
                Context = context;
            else
                throw new ArgumentNullException("context");
        }

        public IQueryable<TEntity> All => Context.Set<TEntity>().AsQueryable();

        public IQueryable<TEntity> AllWithNoTracking => Context.Set<TEntity>().AsNoTracking();

        public void Attach(TEntity entity)
        {
            Context.Set<TEntity>().Attach(entity);
        }

        public void AttachRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                Context.Set<TEntity>().Attach(entity);
        }

        public IQueryable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = All;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public void Create(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            Context.Set<TEntity>().Add(entity);
        }

        public void CreateRange(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            Context.Set<TEntity>().AddRange(entities);
        }

        public void DeleteRange()
        {
            Context.Set<TEntity>().RemoveRange(All);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            Context.Set<TEntity>().Attach(entity);
            Context.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            Context.Set<TEntity>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            foreach (var entity in entities)
            {
                Context.Set<TEntity>().Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;
            }
        }

        public int Save()
        {
            try
            {
                Context.ApplyStateChanges();
                var result = Context.SaveChanges();
                Context.ResetStates();
                return result;
            }
            catch (Exception ex)
            {
                string exceptionMessage = ex.Message;
                throw;
            }
        }

        public async Task<int> SaveAsync()
        {
            try
            {
                Context.ApplyStateChanges();
                var result = await Context.SaveChangesAsync();
                Context.ResetStates();
                return result;
            }
            catch (Exception ex)
            {
                string exceptionMessage = ex.Message;
                throw;
            }
        }
    }
}