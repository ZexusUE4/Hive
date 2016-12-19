using Hive.DAL;
using Hive.Models;
using Microsoft.AspNet.Identity;
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
        public string Code { get; set; }

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

        public bool IsLeaderLoggedIn()
        {
            return TeamLeader.Id == HttpContext.Current.User.Identity.GetUserId();
        }

        public bool IsMemberLoggedIn()
        {
            return TeamMembers.Count(tm => tm.MemberID == HttpContext.Current.User.Identity.GetUserId()) > 0;
        }

        public bool IsMemberExists(string userId)
        {
            return TeamMembers.Count(tm => tm.MemberID == userId) > 0;
        }

        public static string GetUniqueCode(HiveContext db)
        {
            Random rand = new Random();
            Team t = null;
            string retCode;
            do
            {
                retCode = "T" + rand.Next(100, 999).ToString();
                t = db.Teams.FirstOrDefault(el => el.Code == retCode);
            } while (t != null);

            return retCode;
        }

    }
}