using System;
using System.Collections.Generic;

namespace XrnCourse.BucketList.WebApi.Domain
{
    public class User : EntityBase<Guid>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public ICollection<Bucketlist> OwnedBuckets { get; set; }
    }
}
