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

    }
}
