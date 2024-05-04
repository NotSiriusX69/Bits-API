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
                return Ok(appInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"error: {ex.Message}");
            }

        }

        // GET all projects for a specific user 
        [HttpGet("get-user-projects")]
        public IActionResult GetUserProjects([FromBody] int id) {

            try
            {
                var projects = _usersService.GetUserProjects(id);

                if(projects == null)
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

                if(users == null)
                {
                    return BadRequest("No Users in database");
                }

                return Ok(users);

            }catch (Exception ex) 
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
            catch(Exception ex)
            {
                return StatusCode(500, $"error: {ex.Message}");
            }
        }

    }
}
