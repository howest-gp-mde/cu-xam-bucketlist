using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using XrnCourse.BucketList.WebApi.Data;
using XrnCourse.BucketList.WebApi.Domain.Services.Abstract;

namespace XrnCourse.BucketList.WebApi.Domain.Services
{
    public class BucketlistRepository : EFRepositoryBase<Bucketlist, Guid>
    {
        public BucketlistRepository(BucketlistContext context) 
            : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Bucketlist>> GetAllByOwner(Guid ownerId)
        {
            return await GetAll(e => e.OwnerId == ownerId)
                .Include(e => e.Items)
                .ToListAsync();
        }

        public async override Task<Bucketlist> Get(Guid id)
        {
            return await context.Set<Bucketlist>()
                .Include(e => e.Items)
                .Include(e => e.Owner)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async override Task<Bucketlist> Update(Bucketlist entity)
        {
            var existingBucketItems = await context.Set<BucketlistItem>()
                .AsNoTracking()
                .Where(e => e.BucketId == entity.Id).AsNoTracking()
                .ToListAsync();

            //delete existing items no longer in incoming collection
            foreach (var existingItem in existingBucketItems)
            {
                if (!entity.Items.Any(e => e.Id == existingItem.Id))
                    context.Remove(existingItem);
            }

            //syncronise incoming bucketlist items (they might be added or modified)
            foreach (var item in entity.Items)
            {
                if (existingBucketItems.Any(e => e.Id == item.Id)) //item already exists?
                    context.Update(item);
                else
                    context.Add(item);
            }

            //ensure update of bucket list itself happens, even when no props are changed...
            context.Entry(entity).State = EntityState.Modified;

            //save all this in one transaction
            await context.SaveChangesAsync();

            return await context.FindAsync<Bucketlist>(entity.Id);
        }
    }
}
