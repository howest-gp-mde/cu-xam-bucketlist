using System;

namespace XrnCourse.BucketList.WebApi.Domain
{
    public class BucketlistItem : EntityBase<Guid>
    {
        public Guid BucketId { get; set; }        //FK for ParentBucket
        public Bucketlist ParentBucket { get; set; }
        public string ItemDescription { get; set; }
        public DateTime? CompletionDate { get; set; }
    }
}
