using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XrnCourse.BucketList.Domain.Models;

namespace XrnCourse.BucketList.Domain.Services.Mocking
{
    /// <summary>
    /// Fake service which holds data in-memory
    /// </summary>
    public class MockBucketsService : IBucketsService
    {
        // holds all bucket lists in memory for testing purproses
        private static List<Bucket> bucketLists = new List<Bucket>
        {
            new Bucket{
                Id = Guid.Parse("11111111-0000-0000-0000-000000000001"),
                OwnerId = Guid.Parse("00000000-0000-0000-0000-000000000001"), //the first user
                Title = "Siegfried's first bucket list",
                Description = "A simple bucket list", ImageUrl = null, IsFavorite = true,
                Items = new List<BucketItem> {
                    new BucketItem {
                        Id = Guid.Parse("11111111-1111-0000-0000-000000000001"),
                        ItemDescription ="Make a world trip", Order = 1 },
                    new BucketItem {
                        Id = Guid.Parse("11111111-2222-0000-0000-000000000002"),
                        ItemDescription="Learn Xamarin", Order = 2,
                        CompletionDate = DateTime.Now },
                    new BucketItem {
                        Id = Guid.Parse("11111111-3333-0000-0000-000000000003"),
                        ItemDescription="Publish my first mobile app", Order = 3
                    }
                }
            }
        };

        public async Task<Bucket> GetBucketList(Guid id)
        {
            var bucket = bucketLists.FirstOrDefault(e => e.Id == id);
            return await Task.FromResult(bucket); //ensures async result
        }

        public async Task<IQueryable<Bucket>> GetBucketListsForUser(Guid userid)
        {
            var buckets = bucketLists.Where(e => e.OwnerId == userid).AsQueryable();
            return await Task.FromResult(buckets); //ensures async result
        }

        public async Task<Bucket> AddBucketList(Bucket bucket)
        {
            bucketLists.Add(bucket);
            return await Task.FromResult(bucket); //ensures async result
        }

        public async Task<Bucket> UpdateBucketList(Bucket bucket)
        {
            var oldBucketList = bucketLists.FirstOrDefault(e => e.Id == bucket.Id);
            bucketLists.Remove(oldBucketList);
            bucketLists.Add(bucket);
            return await Task.FromResult(bucket); //ensures async result
        }

        public async Task<Bucket> DeleteBucketList(Guid id)
        {
            var oldBucketList = bucketLists.FirstOrDefault(e => e.Id == id);
            bucketLists.Remove(oldBucketList);
            return await Task.FromResult(oldBucketList); //ensures async result
        }
    }
}
