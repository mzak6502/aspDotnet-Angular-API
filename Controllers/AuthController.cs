using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;

        }

        // POST api/values
        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password)
        {
            // [ToDO] Validate Request

            username = username.ToLower();

            if (await _repo.UserExists(username))
                return BadRequest("username already exists");
            
            var userToCreate = new User
            {
                Username = username
            };

            var createdUser = await _repo.Register(userToCreate, password);

            // return CreatedAtRoute()
            return StatusCode(201);
        }
    }
}