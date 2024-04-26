using System.ComponentModel.DataAnnotations;

namespace Bits_API.Models
{
    // DTO : Data Transfer Object
    public class UserLogin
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}
