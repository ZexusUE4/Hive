using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hive.Models
{
    public enum TaskStatuses
    {
        Active,
        Completed,
        Expired,
    }

    public enum TaskPriorites
    {
        Low,
        Medium,
        High,
    }

    public class Task
    {
        public Task()
        {
            Assignees = new HashSet<Team>();
        }

        public int ID { get; set; }
        public int ProjectID { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        public TaskStatuses Status { get; set; }
        public TaskPriorites Priority { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime DateAssigned { get; set; }

        public virtual Project Project { get; set; }
        public virtual ICollection<Team> Assignees { get; set; }

    }
}