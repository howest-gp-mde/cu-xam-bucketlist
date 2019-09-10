using System;

namespace XrnCourse.BucketList.Domain.Models
{
    public class AppSettings
    {
        public Guid CurrentUserId { get; set; }
        public bool EnableListSharing { get; set; }
        public bool EnableNotifications { get; set; }
    }
}
