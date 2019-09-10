using System;
using System.Linq;
using System.Threading.Tasks;
using XrnCourse.BucketList.Domain.Models;

namespace XrnCourse.BucketList.Domain.Services
{
    /// <summary>
    /// Provides functionalities to manage <see cref="Bucket"/> instances in the domain
    /// </summary>
    public interface IBucketsService
    {
        /// <summary>
        /// Gets a Bucket instance by its ID
        /// </summary>
        /// <param name="id">The id of a Bucket instance</param>
        /// <returns>a Bucket instance or <see langword="null"/> if it doesn't exist</returns>
        Task<Bucket> GetBucketList(Guid id);

        /// <summary>
        /// Retrieves all Bucket instances for a given owner ID
        /// </summary>
        /// <param name="userid">The id of the User who owns the Bucket instances to find</param>
        /// <returns>A Queryable collection of Bucket instances owned by the corresponding User instance</returns>
        Task<IQueryable<Bucket>> GetBucketListsForUser(Guid userid);

        /// <summary>
        /// Updates an existing Bucket instance in the data store
        /// </summary>
        /// <param name="bucket">The Bucket instance to persist in the data store</param>
        /// <returns>The updated Bucket instance on success</returns>
        Task<Bucket> UpdateBucketList(Bucket bucket);

        /// <summary>
        /// Adds a Bucket instance to the data store
        /// </summary>
        /// <param name="bucket">The Bucket instance to persist in the data store</param>
        /// <returns>The added Bucket instance on success</returns>
        Task<Bucket> AddBucketList(Bucket bucket);

        /// <summary>
        /// Deletes an existing Bucket instance from the data store
        /// </summary>
        /// <param name="id">The id of a Bucket instance</param>
        /// <returns>The deleted Bucket instance</returns>
        Task<Bucket> DeleteBucketList(Guid id);
    }
}
