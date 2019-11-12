using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using XrnCourse.BucketList.WebApi.Data;

namespace XrnCourse.BucketList.WebApi.Domain.Services.Abstract
{
    public abstract class EFRepositoryBase<TModel, TKey> : IRepository<TModel, TKey>
        where TModel : EntityBase<TKey>
    {
        protected BucketlistContext context;

        public EFRepositoryBase(BucketlistContext context)
        {
            this.context = context;
        }

        public virtual async Task<bool> Exists(TKey id)
        {
            return await context.Set<TModel>().AnyAsync(e => e.Id.Equals(id));
        }

        public virtual async Task<TModel> Get(TKey id)
        {
            return await context.Set<TModel>().FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public virtual IQueryable<TModel> GetAll(Expression<Func<TModel, bool>> predicate)
        {
            return context.Set<TModel>().Where(predicate);
        }

        public virtual async Task<int> Count(Expression<Func<TModel, bool>> predicate)
        {
            return await context.Set<TModel>().CountAsync(predicate);
        }

        public virtual async Task<TModel> Update(TModel entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
            return await context.FindAsync<TModel>(entity.Id);
        }

        public virtual async Task<TModel> Insert(TModel entity)
        {
            context.Add(entity);
            await context.SaveChangesAsync();
            return await context.FindAsync<TModel>(entity.Id);
        }

        public virtual async Task<TModel> Delete(TKey id)
        {
            TModel entity = await context.FindAsync<TModel>(id);
            context.Remove(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
