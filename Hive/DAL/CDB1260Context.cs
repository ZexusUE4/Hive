using Hive.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Hive.DAL
{
    public class HiveContext : IdentityDbContext<HiveUser>
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Team> Teams { get; set; }

        public HiveContext() : base("HiveContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Added for Identity purpose
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    public class CDB1260Initializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<HiveContext>
    {
        protected override void Seed(HiveContext context)
        {
            //var people = new List<Person>
            //{
            //new Person{FirstName="Ahmed Adel Salama",LastName="Abdellah",ArabicFullName="Ahmed Adel in Arabic",DateOfBirth=DateTime.Parse("1987-01-29")},
            //new Person{FirstName="Karim Adel Salama",LastName="Adel",ArabicFullName="Karim Adel in Arabic",DateOfBirth=DateTime.Parse("1988-12-31")}            
            //};

            //people.ForEach(s => context.People.Add(s));
            //context.SaveChanges();
        }
    }
}