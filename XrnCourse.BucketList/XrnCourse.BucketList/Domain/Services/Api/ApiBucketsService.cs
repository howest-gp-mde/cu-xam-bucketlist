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
        private readonly string _baseUri;

        public ApiBucketsService()
        {
            _baseUri = "https://enter-your-lan-ip-here:5001/";
        }

        public async Task<Bucket> AddBucketList(Bucket bucket)
        {
            // add bucket 
            var addedBucket = await WebApiClient
                .PostCallApi<Bucket, Bucket>($"{_baseUri}bucketlists", bucket);
            return addedBucket;
        }

        public async Task<Bucket> DeleteBucketList(Guid id)
        {
            return await WebApiClient
                .DeleteCallApi<Bucket>($"{_baseUri}bucketlists/{id}");
        }

        public async Task<Bucket> GetBucketList(Guid id)
        {
            return await WebApiClient
                .GetApiResult<Bucket>($"{_baseUri}bucketlists/{id}");
        }

        public async Task<IQueryable<Bucket>> GetBucketListsForUser(Guid userid)
        {
            var bucketLists = await WebApiClient
                .GetApiResult<IEnumerable<Bucket>>($"{_baseUri}users/{userid}/bucketlists");
            return bucketLists.AsQueryable();
        }

        public async Task<Bucket> UpdateBucketList(Bucket bucket)
        {
            return await WebApiClient
                .PutCallApi<Bucket, Bucket>($"{_baseUri}bucketlists/{bucket.Id}", bucket);
        }
    }
}
