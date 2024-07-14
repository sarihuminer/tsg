using Demo.Domain.Model.Data;
using Demo.Repositories.Implementations.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntityController : ControllerBase
    {
        public ICacheRepository<Person> _cacheRepository { get; set; }

        public EntityController(ICacheRepository<Person> cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Person>> GetOrAdd([FromBody] Person person)
        {
            try
            {
                return Ok(await _cacheRepository.CacheEntityAsync(person));
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }
    }
}
