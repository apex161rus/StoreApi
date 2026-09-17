using Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StoreController : ControllerBase
    {
        protected readonly AppDbContext DbContext;

        protected StoreController(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
