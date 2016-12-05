using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hive.Models
{
    public enum ProjectStatuses
    {
        Active,
        [Display(Name = "In Active")]
        InActive,
        Completed,
    }

    public class Project
    {

        private static List<string> Colors = new List<string>()
        {
            "coral",
            "darkblue",
            "darkmagenta",
            "red"
        };

        private static Random rand = new Random();

        public static string GetRandomColor()
        {
            return Colors[rand.Next(Colors.Count)];
        }

        public int ID { get; set; }
        public string ManagerID { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public ProjectStatuses Status { get; set; }
        public string Color { get; set; }

        public virtual HiveUser Manager { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection <Task> Tasks { get; set; }
    }
}