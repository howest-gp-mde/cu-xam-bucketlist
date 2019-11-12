using System;
using System.Collections.Generic;

namespace XrnCourse.BucketList.WebApi.Domain
{
    public class Bucketlist : EntityBase<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsFavorite { get; set; }
        public Guid OwnerId { get; set; }       //unshadowed FK
        public User Owner { get; set; }
        public ICollection<BucketlistItem> Items { get; set; } = new List<BucketlistItem>();
    }
}
