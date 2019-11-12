using AutoMapper;
using System.Collections.Generic;
using XrnCourse.BucketList.WebApi.Domain;
using XrnCourse.BucketList.WebApi.Dto;

namespace XrnCourse.BucketList.WebApi.Extensions
{
    public static class BucketlistDtoExtensions
    {
        public static BucketlistDto ToDto(this Bucketlist model, IMapper mapper)
        {
            return mapper.Map<BucketlistDto>(model);
        }

        public static IEnumerable<BucketlistDto> ToDto(this IEnumerable<Bucketlist> collection, IMapper mapper)
        {
            return mapper.Map<IEnumerable<BucketlistDto>>(collection);
        }
    }
}
