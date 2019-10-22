using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using XrnCourse.BucketList.Domain.Models;

namespace XrnCourse.BucketList.Domain.Services.Local
{
    public class JsonBucketsService : IBucketsService
    {
        private readonly string _filePath;

        public JsonBucketsService()
        {
            _filePath = Path.Combine(FileSystem.AppDataDirectory, "bucketlists.json");
        }

        public async Task<Bucket> AddBucketList(Bucket bucket)
        {
            var bucketlists = await GetAllBucketlists();
            bucketlists.Add(bucket);
            SaveBucketsToJsonFile(bucketlists);
            return await GetBucketList(bucket.Id);
        }

        public async Task<Bucket> DeleteBucketList(Guid id)
        {
            var bucketlists = await GetAllBucketlists();
            var bucketlistToRemove = bucketlists.FirstOrDefault(e => e.Id == id);
            bucketlists.Remove(bucketlistToRemove);
            SaveBucketsToJsonFile(bucketlists);
            return bucketlistToRemove;
        }

        public async Task<Bucket> GetBucketList(Guid id)
        {
            var bucketLists = await GetAllBucketlists();
            return bucketLists.FirstOrDefault(e => e.Id == id);
        }

        public async Task<IQueryable<Bucket>> GetBucketListsForUser(Guid userid)
        {
            var bucketLists = await GetAllBucketlists();
            return bucketLists.Where(e => e.OwnerId == userid).AsQueryable();
        }

        public async Task<Bucket> UpdateBucketList(Bucket bucket)
        {
            await DeleteBucketList(bucket.Id);
            return await AddBucketList(bucket);
        }

        protected async Task<IList<Bucket>> GetAllBucketlists()
        {
            try
            {
                string bucketListsJson = File.ReadAllText(_filePath);
                var bucketLists = JsonConvert.DeserializeObject<IList<Bucket>>(bucketListsJson);
                return await Task.FromResult(bucketLists);  //using Task.FromResult to have atleast one await in this async method
            }
            catch  //return empty collection on file not found, deserialization error, ...
            {
                return (new List<Bucket>());
            }
        }

        protected void SaveBucketsToJsonFile(IEnumerable<Bucket> buckets)
        {
            string bucketsJson = JsonConvert.SerializeObject(buckets);
            File.WriteAllText(_filePath, bucketsJson);
        }
    }
}
