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
    public class BucketlistsController : ControllerCrudBase<Bucketlist, Guid, BucketlistRepository>
    {
        private readonly ILogger<BucketlistsController> _logger;
        protected readonly IMapper _mapper;

        public BucketlistsController(BucketlistRepository repository, ILogger<BucketlistsController> logger, IMapper mapper)
            : base(repository)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<IActionResult> Get()
        {
            //don't allow getting all bucket lists without specifying a user
            return await Task.FromResult(BadRequest());
        }

        [HttpGet("owner/{ownerId}")]
        public async Task<IActionResult> GetByOwner([FromRoute] Guid ownerId)
        {
            var bucketlists = await _repository.GetAllByOwner(ownerId);
            return Ok(bucketlists.ToDto(_mapper));
        }
    }
}
