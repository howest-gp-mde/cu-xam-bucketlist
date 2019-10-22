using System;

namespace XrnCourse.BucketList.Domain.Models
{
    public class BucketItem
    {
        public Guid Id { get; set; }
        public Guid BucketId { get; set; }
        public Bucket ParentBucket { get; set; }
        public string ItemDescription { get; set; }
        public int Order { get; set; }
        public DateTime? CompletionDate { get; set; }
    }
}
