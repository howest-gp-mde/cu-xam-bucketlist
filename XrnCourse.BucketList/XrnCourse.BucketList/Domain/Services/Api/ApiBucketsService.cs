using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using XrnCourse.BucketList.Domain.Models;

namespace XrnCourse.BucketList.Domain.Services.Api
{
    public class ApiBucketsService : IBucketsService
    {
        private readonly IApiClient _client;

        public ApiBucketsService(IApiClient client)
        {
            _client = client;
        }

        public async Task<Bucket> AddBucketList(Bucket bucket)
        {
            // add bucket 
            var addedBucket = await _client
                .PostCallApi<Bucket, Bucket>($"{Constants.ApiBaseUrl}bucketlists", bucket);
            return addedBucket;
        }

        public async Task<Bucket> DeleteBucketList(Guid id)
        {
            return await _client
                .DeleteCallApi<Bucket>($"{Constants.ApiBaseUrl}bucketlists/{id}");
        }

        public async Task<Bucket> GetBucketList(Guid id)
        {
            return await _client
                .GetApiResult<Bucket>($"{Constants.ApiBaseUrl}bucketlists/{id}");
        }

        public async Task<IQueryable<Bucket>> GetBucketListsForUser(Guid userid)
        {
            var bucketLists = await _client
                .GetApiResult<IEnumerable<Bucket>>($"{Constants.ApiBaseUrl}users/{userid}/bucketlists");
            return bucketLists.AsQueryable();
        }

        public async Task<Bucket> UpdateBucketList(Bucket bucket)
        {
            return await _client
                .PutCallApi<Bucket, Bucket>($"{Constants.ApiBaseUrl}bucketlists/{bucket.Id}", bucket);
        }
    }
}
