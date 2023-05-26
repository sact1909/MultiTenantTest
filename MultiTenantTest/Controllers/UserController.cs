using Microsoft.AspNetCore.Mvc;
using MultiTenantTest.Entities;

namespace MultiTenantTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController<TUser>
    {
        public UserController(TestDbContext dbContext) : base(dbContext)
        {
        }

    }
}
