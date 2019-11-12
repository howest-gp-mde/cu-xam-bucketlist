using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace XrnCourse.BucketList.WebApi.Domain.Services.Abstract
{
    public interface IRepository<TModel, TKey> where TModel : EntityBase<TKey>
    {
        Task<bool> Exists(TKey id);
        Task<TModel> Get(TKey id);
        IQueryable<TModel> GetAll(Expression<Func<TModel, bool>> predicate);
        Task<int> Count(Expression<Func<TModel, bool>> predicate);
        Task<TModel> Update(TModel entity);
        Task<TModel> Insert(TModel entity);
        Task<TModel> Delete(TKey id);
    }
}
