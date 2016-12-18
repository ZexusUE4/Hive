using Hive.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hive.Models
{
    public class Team : Colorable
    {
        public Team()
        {
            TeamMembers = new HashSet<TeamMember>();
            Projects = new HashSet<Project>();
            Tasks = new HashSet<Task>();
        }

        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public string Description { get; set; }
        public bool IsDefaultTeam { get; set; }

        public virtual ICollection<TeamMember> TeamMembers { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }

        public HiveUser TeamLeader
        {
            get
            {
                return TeamMembers.FirstOrDefault(tm => tm.IsLeader).Member;
            }
        }

        public IEnumerable<HiveUser> AcceptedMembers
        {
            get
            {
                return TeamMembers.Where(tm => tm.Status == TeamMemberStatuses.Accepted).Select(tm => tm.Member);
            }
        }

        public IEnumerable<HiveUser> PendingMembers
        {
            get
            {
                return TeamMembers.Where(tm => tm.Status == TeamMemberStatuses.Pending).Select(tm => tm.Member);
            }
        }

        public IEnumerable<HiveUser> AllMembers
        {
            get
            {
                return TeamMembers.Select(tm => tm.Member);
            }
        }

    }
}