namespace Bits_API.Models
{
    public class User
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public bool isAdmin { get; set; }
        public string createdAt { get; set; }

        public List<Project>? projects { get; set; }

        public User() {
            createdAt = DateTime.Now.ToString();
        }

    }
}
