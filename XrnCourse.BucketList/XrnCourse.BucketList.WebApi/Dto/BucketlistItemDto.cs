using System;

namespace XrnCourse.BucketList.WebApi.Dto
{
    public class BucketlistItemDto
    {
        public Guid Id { get; set; }
        public Guid BucketId { get; set; }
        public string ItemDescription { get; set; }
        public DateTime? CompletionDate { get; set; }
    }
}
