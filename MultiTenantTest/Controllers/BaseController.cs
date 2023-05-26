using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenantTest.Entities;

namespace MultiTenantTest.Controllers
{
    public class BaseController<TEntity> : ControllerBase where TEntity : class
    {
        private TestDbContext _dbContext;
        public BaseController(TestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(Guid id)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return entity;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> GetAll([FromHeader(Name= "X-TenantId")] string tenantId)
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<TEntity>> Create(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
