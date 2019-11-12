using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using XrnCourse.BucketList.WebApi.Domain;
using XrnCourse.BucketList.WebApi.Domain.Services;
using XrnCourse.BucketList.WebApi.Extensions;

namespace XrnCourse.BucketList.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerCrudBase<User, Guid, UserRepository>
    {
        private readonly ILogger<UsersController> _logger;
        protected readonly IMapper _mapper;

        public UsersController(UserRepository repository, ILogger<UsersController> logger, IMapper mapper)
            : base(repository)
        {
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("{ownerId}/bucketlists")]
        public async Task<IActionResult> GetBucketListsByOwner([FromRoute] Guid ownerId)
        {
            var bucketlists = await _repository.GetBucketlistsForOwner(ownerId);
            return Ok(bucketlists.ToDto(_mapper));
        }

    }
}