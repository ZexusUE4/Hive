using Hive.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hive.Models
{
    public class Team
    {
        public Team()
        {
            Members = new HashSet<HiveUser>();
            Projects = new HashSet<Project>();
            Tasks = new HashSet<Task>();
        }

        public int ID { get; set; }
        public string TeamLeaderID { get; set; }

        [Required]
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public string Description { get; set; }

        [ForeignKey("TeamLeaderID")]
        public virtual HiveUser TeamLeader { get; set; }
        public virtual ICollection<HiveUser> Members { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}