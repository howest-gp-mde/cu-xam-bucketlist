using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XrnCourse.BucketList.WebApi.Data;
using XrnCourse.BucketList.WebApi.Domain.Services.Abstract;

namespace XrnCourse.BucketList.WebApi.Domain.Services
{
    public class UserRepository : EFRepositoryBase<User, Guid>
    {
        public UserRepository(BucketlistContext context) 
            : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Bucketlist>> GetBucketlistsForOwner(Guid ownerId)
        {
            return await GetAll(e => e.Id == ownerId)
                .Include(e => e.OwnedBuckets)
                .ThenInclude(b => b.Items)
                .SelectMany(e => e.OwnedBuckets)
                .ToListAsync();
        }
    }
}
