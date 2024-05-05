using Bits_API.Models.DTOs;
using Bits_API.Models.Entities;
using Bits_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Bits_API.Controllers
{

    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UserService _usersService;


        public UsersController(UserService usersService)
        {
            _usersService = usersService;
        }

        // POST register user
        [HttpPost("/register")]
        public IActionResult CreateUser([FromBody] User user)
        {
            try
            {
                // Check if model is valid
                if (ModelState.IsValid)
                {


                    // Set Cookie
                    var cookieOptions = new CookieOptions
                    {
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        MaxAge = TimeSpan.FromMinutes(30)

                    };

                    var User = _usersService.CreateNewUser(user);

                    // Send the Cookie with user_id
                    Response.Cookies.Append("user_id", User.userId.ToString(), cookieOptions);

                    HttpContext.Session.SetString("user_id", User.userId.ToString());
                    HttpContext.Session.SetString("isAdmin", User.isAdmin.ToString());


                    return Ok("User Created Succesfully");
                }
                return BadRequest("Invalid User Data");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"error: {ex.Message}");
            }
        }

        // POST login user
        [HttpPost("/login")]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            try
            {

                // Retrieve user
                var User = _usersService.GetUserByEmailPassword(userLogin.email, userLogin.password);

                // Set Cookie
                var cookieOptions = new CookieOptions
                {
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    MaxAge = TimeSpan.FromMinutes(30)

                };

                // Send the Cookie with user_id
                Response.Cookies.Append("user_id", User.userId.ToString(), cookieOptions);

                if (User == null)
                {
                    return Unauthorized("Invalid email or password");
                }
                else
                {

                    // If admin 
                    if (User.isAdmin)
                    {
                        HttpContext.Session.SetString("user_id", User.userId.ToString());
                        HttpContext.Session.SetString("isAdmin", User.isAdmin.ToString());
                        return Ok(User.isAdmin);
                    }
                    else
                    {
                        HttpContext.Session.SetString("user_id", User.userId.ToString());
                        HttpContext.Session.SetString("isAdmin", User.isAdmin.ToString());
                        return Ok(User.isAdmin);
                    }

                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"error: {ex.Message}");
            }

        }

        // GET all user info by id
        [HttpGet("/get-user-info")]
        public IActionResult GetUserInfo([FromQuery] int id)
        {

            try
            {

                var user = _usersService.GetUserById(id);

                if (user == null)
                {
                    return NotFound("User Not found");
                }

                // return user if found

                return Ok(user);


            }
            catch (Exception ex)
            {
                return StatusCode(500, $"error: {ex.Message}");
            }
        }

        [HttpPost("/get-user-data")]
        public IActionResult GetUserData([FromBody] int id)
        {

            try
            {
                Console.WriteLine("res" + id);

                var user = _usersService.GetUserById(id);

                UserDataSimplified userDataSimplified = new UserDataSimplified
                {
                    userId = user.userId,
                    isAdmin = user.isAdmin
                };

                if (user == null)
                {
                    return NotFound("User Not found");
                }

                // return user if found
                return Ok(userDataSimplified);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"error: {ex.Message}");
            }
        }


    }
}
