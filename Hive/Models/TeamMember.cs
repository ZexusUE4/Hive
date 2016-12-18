using Hive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hive.Models
{
    public enum TeamMemberStatuses
    {
        Pending,
        Accepted,
    }

    public class TeamMember
    {
        public int ID { get; set; }
        public int TeamID { get; set; }
        public int MemberID { get; set; }
        public TeamMemberStatuses Status { get; set; }
        public bool IsLeader { get; set; }

        public virtual Team Team { get; set; }
        public virtual HiveUser Member { get; set; }
    }
}