using System;
using System.Threading.Tasks;
using XrnCourse.BucketList.Domain.Models;

namespace XrnCourse.BucketList.Domain.Services
{
    /// <summary>
    /// Provides functionalities to manage <see cref="User"/> instances in the domain
    /// </summary>
    public interface IUsersService
    {
        /// <summary>
        /// Gets a User instance by its ID
        /// </summary>
        /// <param name="id">The id of a User instance</param>
        /// <returns>a User instance or <see langword="null"/> if it doesn't exist</returns>
        Task<User> GetUser(Guid id);

        /// <summary>
        /// Updates an existing User instance in the data store
        /// </summary>
        /// <param name="user">The User instance to persist in the data store</param>
        /// <returns>The updated User instance on success</returns>
        Task<User> UpdateUser(User user);

        /// <summary>
        /// Creates a new User instance in the data store
        /// </summary>
        /// <returns>The new User instance</returns>
        Task<User> CreateUser(User user);

    }
}
