using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hive.Models
{
    public enum ProjectStatuses
    {
        Active,
        [Display(Name = "In Active")]
        InActive,
        Completed,
    }

    public class Project : Colorable
    {

        public int ID { get; set; }
        public string ManagerID { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public ProjectStatuses Status { get; set; }

        public virtual HiveUser Manager { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection <Task> Tasks { get; set; }

        public bool IsLoggedManager()
        {
            return ManagerID == HttpContext.Current.User.Identity.GetUserId();
        }

        public IEnumerable<Task> ActiveTasks
        {
            get
            {
                return Tasks.Where(t => t.Status == TaskStatuses.Active);
            }
        }

        public IEnumerable<Task> CompletedTasks
        {
            get
            {
                return Tasks.Where(t => t.Status == TaskStatuses.Completed);
            }
        }

        public IEnumerable<Task> ExpiredTasks
        {
            get
            {
                return Tasks.Where(t => t.Status == TaskStatuses.Expired);
            }
        }

    }
}