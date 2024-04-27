using Bits_API.Models.DTOs;
using Bits_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bits_API.Controllers
{
    
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly AdminService _adminService;

        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("/get-app-info")]
        public IActionResult Get()
        {
            ApplicationInfo appInfo = _adminService.GetApplicationInfo();

            return Ok(appInfo);
        }

    }
}
