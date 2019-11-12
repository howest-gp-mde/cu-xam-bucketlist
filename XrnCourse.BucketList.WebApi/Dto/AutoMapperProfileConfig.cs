using AutoMapper;
using System.Linq;
using XrnCourse.BucketList.WebApi.Domain;

namespace XrnCourse.BucketList.WebApi.Dto
{
    public class AutoMapperProfileConfig : Profile
    {
        public AutoMapperProfileConfig() : this("DefaultProfile")
        {

        }

        protected AutoMapperProfileConfig(string profileName) : base(profileName)
        {
            CreateMap<BucketlistDto, Bucketlist>().ReverseMap();
            CreateMap<BucketlistItemDto, BucketlistItem>().ReverseMap();
        }
    }
}
