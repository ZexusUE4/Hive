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

namespace Hive.Models
{
    public enum Genders
    {
        Male,
        Female,
    }

    public class HiveUser : IdentityUser
    {
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