﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Bits_API.Models.Entities
{
    public class Task
    {
        public int taskId { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public string? priority { get; set; }
        public string createdAt { get; set; }
        public string? endDate { get; set; }

        public int projectId { get; set; }
        public Project project { get; set; }

        [ForeignKey("Status")]
        public int statusId { get; set; }
        public Status status { get; set; }

        public Task()
        {
            createdAt = DateTime.Now.ToString();
        }
    }
}
