using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace XrnCourse.BucketList.Domain.Services.Api
{
    public class CustomHttpClient : HttpClient, IApiClient
    {
        public CustomHttpClient() : base(CreateClientHandler())
        {

        }

        private JsonMediaTypeFormatter GetJsonFormatter()
        {
            var formatter = new JsonMediaTypeFormatter();
            //prevent self-referencing loops when saving Json (Bucket -> BucketItem -> Bucket -> ...)
            formatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return formatter;
        }

        private static HttpClientHandler CreateClientHandler()
        {
            var httpClientHandler = new HttpClientHandler();
#if DEBUG
            //allow connecting to untrusted certificates when running a DEBUG assembly
            httpClientHandler.ServerCertificateCustomValidationCallback =
                (message, cert, chain, errors) => { return true; };
#endif
            return httpClientHandler;
        }

        public async Task<T> GetApiResult<T>(string uri)
        {
            string response = await GetStringAsync(uri);
            return JsonConvert.DeserializeObject<T>(response, GetJsonFormatter().SerializerSettings);
        }

        public async Task<TOut> PutCallApi<TOut, TIn>(string uri, TIn entity)
        {
            return await CallApi<TOut, TIn>(uri, entity, HttpMethod.Put);
        }

        public async Task<TOut> PostCallApi<TOut, TIn>(string uri, TIn entity)
        {
            return await CallApi<TOut, TIn>(uri, entity, HttpMethod.Post);
        }

        public async Task<TOut> DeleteCallApi<TOut>(string uri)
        {
            return await CallApi<TOut, object>(uri, null, HttpMethod.Delete);
        }

        private async Task<TOut> CallApi<TOut, TIn>(string uri, TIn entity, HttpMethod httpMethod)
        {
            TOut result = default;

            HttpResponseMessage response;
            if (httpMethod == HttpMethod.Post)
            {
                response = await this.PostAsync(uri, entity, GetJsonFormatter());
            }
            else if (httpMethod == HttpMethod.Put)
            {
                response = await this.PutAsync(uri, entity, GetJsonFormatter());
            }
            else
            {
                response = await DeleteAsync(uri);
            }
            result = await response.Content.ReadAsAsync<TOut>();

            return result;
        }
    }
}
