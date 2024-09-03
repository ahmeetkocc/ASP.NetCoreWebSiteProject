using AuthAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        //GetUserList
        [HttpGet("UserList")]
        public IActionResult GetUsers()
        {
            var userList = _appDbContext.Users
                .Include(u => u.Role)
                .Include(u => u.RefreshTokens)
                .Select(u => new {
                    u.Id,
                    u.Username,
                    u.Email,
                    RoleName = u.Role.Name,
                    u.RoleId,
                    RefreshTokens = u.RefreshTokens.Select(x => x.Token)
                })
                .ToList();
            return Ok(userList);
        }

        //DeleteUser
        [HttpPost("delete/{userId}")]
        public IActionResult Delete([FromRoute] int userId)
        {
            var user = _appDbContext.Users
                .Include(u => u.Role)
                .Include(u => u.RefreshTokens)
                .SingleOrDefault(u => u.Id == userId);

            if (user is null)
                return NoContent();

            //İlişkili yerlerin nasıl silineceği ile ilgili hata alındı.
            _appDbContext.RefreshTokens.RemoveRange(user.RefreshTokens);

            _appDbContext.Users.Remove(user);
            _appDbContext.SaveChanges();

            return Ok();
        }
    }
}
