using Bits_API.Models.DTOs;
using Bits_API.Models.Entities;
using Bits_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("/register")]
        public IActionResult CreateUser([FromBody] User user)
        {
            try
            {
                // Check if model is valid
                if (ModelState.IsValid)
                {
                    var User = _usersService.CreateNewUser(user);
                    return Ok("User Created Succesfully");
                }
                return BadRequest("Invalid User Data");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"error: {ex.Message}");
            }
        }

        [HttpPost("/login")]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            try
            {
                if (ModelState.IsValid) {

                    // Retrieve user
                    var User = _usersService.GetUserByEmailPassword(userLogin.email, userLogin.password);

                    if (User == null)
                    {
                        return Unauthorized("Invalid email or password");
                    }
                    else
                    {
                        // If admin 
                        if (User.isAdmin)
                        {
                            return Ok("Logged In succesfully admin");
                        }
                        else
                        {  
                            return Ok("Logged In succesfully");
                        }

                    }
                }
                return BadRequest("Invalid User Data");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"error: {ex.Message}");
            }

        }

        [HttpGet("/get-user-info")]
        public IActionResult GetUserInfo([FromBody] int id) {

            try { 


                  var user = _usersService.GetUserById(id);

                    if(user == null)
                    {
                        return NotFound("User Not found");
                    }

                    // return user if found

                    return Ok(user);
             

            } catch (Exception ex)
            {
                return StatusCode(500, $"error: {ex.Message}");
            }
        }


    }
}
