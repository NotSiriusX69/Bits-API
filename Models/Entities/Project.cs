using System.ComponentModel.DataAnnotations.Schema;

namespace Bits_API.Models.Entities
{
    public class Project
    {
        public int projectId { get; set; }

        public string name { get; set; }
        public string description { get; set; }
        public string createdAt { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }

        [ForeignKey("Category")]
        public int categoryId { get; set; }
        public Category Category { get; set; }

        public int userId { get; set; }
        public User user { get; set; }

        public List<Task>? tasks { get; set; }
    }
}
