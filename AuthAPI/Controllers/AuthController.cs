using AuthAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }

        //GetUser
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUser([FromRoute] int userId)
        {
            var user = _appDbContext.Users.Find(userId);

            if (user is null)
                return BadRequest("Kullanıcı bulunamadi");

            return Ok(user);
        }
    }
}
