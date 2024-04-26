using Bits_API.Models;
using Bits_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bits_API.Controllers
{
    [Route("bits")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private const string SessionName = "_Name";

        private readonly BitsContext _bitsContext;
        public UsersController(BitsContext bitsContext)
        {
            _bitsContext = bitsContext;
        }

        [HttpPost("register")]
        public IActionResult CreateUser([FromBody] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Check If email already exists
                    bool isEmailExist = _bitsContext.user.Any(u => u.email.Equals(user.email));

                    if (isEmailExist)
                    {
                        return Unauthorized("Email already exists");
                    }

                    _bitsContext.user.Add(user);
                    _bitsContext.SaveChanges();
                    return Ok("User Created Succesfully");
                }
                return BadRequest("Invalid User Data");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"error: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            // retrieve user
            var user = _bitsContext.user.SingleOrDefault( u => u.email.Equals(userLogin.email) && u.password.Equals(userLogin.password));

            if (user == null) 
            {
                return Unauthorized("Invalid email or password");
            }

            HttpContext.Session.SetString(SessionName, user.userName);

            return Ok("Good");
        }

    }
}
