using System.Threading.Tasks;

namespace XrnCourse.BucketList.Domain.Services.Api
{
    public interface IApiClient
    {
        Task<TOut> DeleteCallApi<TOut>(string uri);
        Task<T> GetApiResult<T>(string uri);
        Task<TOut> PostCallApi<TOut, TIn>(string uri, TIn entity);
        Task<TOut> PutCallApi<TOut, TIn>(string uri, TIn entity);
    }
}