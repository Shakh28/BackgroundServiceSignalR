using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApi.Data;

namespace ShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromServices] AppDbContext context)
        {
            var users = await context.Users!.Include(u => u.Contracts)
                .ToListAsync();

            return Ok(users);
        }
        
        [HttpGet("{userId}/contracts")]
        public async Task<IActionResult> GetUserContracts(int userId, [FromServices] AppDbContext context)
        {
            var contracts = await context.Contracts!.Where(u => u.UserId == userId)
                .ToListAsync();

            return Ok(contracts);
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetContracts([FromServices] AppDbContext context)
        {
            var contracts = await context.Contracts!.ToListAsync();
            return Ok(contracts);
        }
    }
}
