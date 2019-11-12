using System;
using System.Collections.Generic;

namespace XrnCourse.BucketList.WebApi.Dto
{
    public class BucketlistDto
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsFavorite { get; set; }
        public ICollection<BucketlistItemDto> Items { get; set; } = new List<BucketlistItemDto>(); //should never be null
    }
}
