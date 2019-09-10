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
    public class MockUsersService : IUsersService
    {
        // holds all users in memory for testing purproses
        private static List<User> users = new List<User>
        {
            new User{
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                UserName = "Siegfried",
                Email="siegfried@bucketlists.test",
            }
        };

        public async Task<User> GetUser(Guid id)
        {
            var user = users.FirstOrDefault(e => e.Id == id);
            return await Task.FromResult(user); //ensures async result
        }

        public async Task<User> UpdateUser(User user)
        {
            var oldUser = users.FirstOrDefault(e => e.Id == user.Id);
            users.Remove(oldUser);
            users.Add(user);
            return await Task.FromResult(oldUser); //ensures async result
        }
    }
}
