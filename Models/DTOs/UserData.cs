using Bits_API.Models.Entities;

namespace Bits_API.Models.DTOs
{
    public class UserData
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public bool isAdmin { get; set; }
        public string createdAt { get; set; }
        public int projectsNumber { get; set; }
    }
}
