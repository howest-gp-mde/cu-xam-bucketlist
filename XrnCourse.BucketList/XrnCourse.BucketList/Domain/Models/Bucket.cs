using System;
using System.Collections.Generic;
using System.Linq;

namespace XrnCourse.BucketList.Domain.Models
{
    public class Bucket
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsFavorite { get; set; }
        public ICollection<BucketItem> Items { get; set; } = new List<BucketItem>(); //should never be null

        public bool IsCompleted => Items.All(i => i.CompletionDate.HasValue);
        public float PercentCompleted
        {
            get
            {
                if (Items.Count > 0)
                    return Items.Count(i => i.CompletionDate.HasValue) / Items.Count;
                else
                    return 0f;
            }
        }
    }

}
