using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using XrnCourse.BucketList.WebApi.Domain;
using XrnCourse.BucketList.WebApi.Domain.Services.Abstract;

namespace XrnCourse.BucketList.WebApi.Controllers
{
    public class ControllerCrudBase<TModel, TKey, TRepository> : ControllerBase
        where TModel : EntityBase<TKey>
        where TRepository : IRepository<TModel, TKey>
    {
        protected TRepository _repository;

        public ControllerCrudBase(TRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            return await Task.FromResult(Ok(_repository.GetAll(e => true)));
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(TKey id)
        {
            var entity = await _repository.Get(id);
            if (entity == null)
                return NotFound();
            else
                return Ok(entity);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put([FromRoute] TKey id, [FromBody] TModel entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id == null || !id.Equals(entity.Id))
            {
                return BadRequest();
            }

            TModel updatedEntity = await _repository.Update(entity);
            if (updatedEntity == null)
            {
                return NotFound();
            }
            return Ok(updatedEntity);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] TModel entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TModel createdEntity = await _repository.Insert(entity);
            if (createdEntity == null)
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete([FromRoute] TKey id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TModel deletedEntity = await _repository.Delete(id);
            if (deletedEntity == null)
            {
                return NotFound();
            }
            return Ok(deletedEntity);
        }
    }
}
