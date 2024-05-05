using Bits_API.Models.DTOs;
using Bits_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Bits_API.Controllers
{
    [Route("admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly AdminService _adminService;
        private readonly UserService _usersService;

        public AdminController(AdminService adminService, UserService usersService)
        {
            _adminService = adminService;
            _usersService = usersService;
        }

        // GET Application Info
        [HttpGet("get-app-info")]
        public IActionResult Get()
        {
            try
            {
                ApplicationInfo appInfo = _adminService.GetApplicationInfo();
                Console.WriteLine(appInfo);

                return Ok(appInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"error: {ex.Message}");
            }

        }

        // GET all projects for a specific user 
        [HttpGet("get-user-projects")]
        public IActionResult GetUserProjects([FromBody] int id)
        {

            try
            {
                var projects = _usersService.GetUserProjects(id);

                if (projects == null)
                {
                    return BadRequest("Projects are Null");
                }

                return Ok(projects);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"error: {ex.Message}");
            }

        }

        // GET all users
        [HttpGet("get-users")]
        public IActionResult GetUsers()
        {

            try
            {
                var users = _adminService.GetUsers();

                if (users == null)
                {
                    return BadRequest("No Users in database");
                }

                return Ok(users);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"error: {ex.Message}");
            }
        }

        // GET all users that match the searching string
        [HttpGet("get-searched-users")]
        public IActionResult GetSearchedUsers([FromQuery] string searchValue)
        {
            try
            {
                var users = _adminService.GetSearchedUsers(searchValue);

                if (users == null)
                {
                    return NotFound("No Users has been found");
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"error: {ex.Message}");
            }
        }

        // GET all user info by id
        [HttpGet("user-info/{id}")]
        public IActionResult GetUserInfo(int id)
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

        // GET all user info by id
        [HttpGet("user-info-projects/{id}")]
        public IActionResult GetUserProjectsInfo(int id)
        {

            try
            {

                var projects = _usersService.GetUserProjects(id);

                if (projects == null || !projects.Any())
                {
                    return NotFound("No Projects were found");
                }

                // return projects if found

                return Ok(projects);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"error: {ex.Message}");
            }
        }

        // UPDATE isAdmin For specific user
        [HttpPut("update-is-admin")]
        public IActionResult UpdateUserIsAdmin([FromQuery] int sender_id, int id)
        {
            var user = _usersService.GetUserById(sender_id);

            if (user.isAdmin == false)
            {
                return BadRequest("You don't have permission");
            }

            var userToUpdate = _usersService.GetUserById(id);

            try
            {
                if (userToUpdate != null)
                {
                    // Reverse isAdmin
                    userToUpdate.isAdmin = !userToUpdate.isAdmin;
                    // Submit changes to db
                    _adminService.SubmitUserChanges();

                    return Ok(userToUpdate);
                }
                else
                {
                    return NotFound("User Not Found");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

    }
}
