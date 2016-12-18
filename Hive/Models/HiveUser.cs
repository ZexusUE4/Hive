using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hive.DAL;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Security.Principal;

namespace Hive.Models
{
    public enum Genders
    {
        Male,
        Female,
    }

    public class HiveUser : IdentityUser
    {

        public HiveUser()
        {
            TeamMembers = new HashSet<TeamMember>();
            Projects = new HashSet<Project>();
        }

        public int DefaultTeamID { get; set; }

        public DateTime JoinedDate { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Profile Picture")]
        public string ProfilePicturePath { get; set; }

        public Genders? Gender { get; set; }
        public string Bio { get; set; }

        public virtual Team DefaultTeam { get; set; }
        public virtual ICollection<TeamMember> TeamMembers { get; set; }
        public virtual ICollection<Project> Projects { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public string GetImagePath()
        {
            if (ProfilePicturePath != null && ProfilePicturePath.Length > 0)
                return ProfilePicturePath;
            else
                return "/App_Data/ProfilePics/man.png";
        }

        public IEnumerable<Team> Teams
        {
            get
            {
                return TeamMembers.Select(tm => tm.Team);
            }
        }
    }


    public class UserRole : IdentityRole
    {
        public UserRole() : base() { }
        public UserRole(string name) : base(name) { }
    }


    public class AccountManager : UserManager<HiveUser>
    {
        public AccountManager(IUserStore<HiveUser> store)
        : base(store)
        {
        }

        // this method is called by Owin therefore best place to configure your User Manager
        public static AccountManager Create(
            IdentityFactoryOptions<AccountManager> options, IOwinContext context)
        {
            var manager = new AccountManager(
                new UserStore<HiveUser>(context.Get<HiveContext>()));

            // optionally configure your manager
            // ...

            return manager;
        }
    }
}


namespace Hive.Models
{
    public static class IdentityExtensions
    {

        public static HiveUser GetHiveUser(this IIdentity identity)
        {
            var userID = ((ClaimsIdentity)identity).GetUserId();

            HiveContext db = new HiveContext();

            HiveUser user = db.Users.Find(userID);

            if (user != null)
            {
                return user;
            }

            return new HiveUser()
            {
                FirstName = "Unknown",
            };
        }

    }
}