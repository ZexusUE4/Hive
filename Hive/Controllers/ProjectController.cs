using Hive.DAL;
using Hive.Models;
using Hive.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hive.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private HiveContext db = new HiveContext();
        
        public ProjectController()
        {

        }

        public ProjectController(HiveContext db)
        {
            this.db = db;
        }

        public ActionResult Index()
        {

            HiveUser user = db.Users.Find(User.Identity.GetUserId());

            return View(user.Projects);
        }

        public ActionResult Create()
        {
            Project project = new Project();

            project.ManagerID = User.Identity.GetUserId();
            project.DateCreated = DateTime.Now;
            project.Status = ProjectStatuses.Active;

            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project model)
        {
            if(ModelState.IsValid)
            {
                db.Projects.Add(model);
                db.SaveChanges();

                return RedirectToAction("Details", new { id = model.ID });
            }

            return View(model);
        }

        public ActionResult Details(int id)
        {
            Project project = db.Projects.Find(id);

            if(project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        public ActionResult Edit(int id)
        {
            Project project = db.Projects.Find(id);

            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Project model)
        {
            if(ModelState.IsValid)
            {
                Project inDb = db.Projects.Find(model.ID);

                db.Entry(inDb).CurrentValues.SetValues(model);
                db.SaveChanges();

                return RedirectToAction("Details", new { id = model.ID });
            }

            return View(model);

        }

        public ActionResult Delete(int id)
        {
            Project project = db.Projects.Find(id);

            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddTeam(int projectID, string teamCode)
        {
            Team team = db.Teams.FirstOrDefault(t => t.Code == teamCode);
            List<Notification> notifications = new List<Notification>();

            if (team != null)
            {
                Project project = db.Projects.Find(projectID);

                if(project.Teams.Contains(team))
                {
                    notifications.Add(new Notification()
                    {
                        Title = "Error !",
                        Content = "Team: " + team.Name + " already exists.",
                        Type = NotificationTypes.Error,
                    });
                }
                else
                {
                    project.Teams.Add(team);

                    db.SaveChanges();

                    notifications.Add(new Notification()
                    {
                        Title = "Success !",
                        Content = "Team: " + team.Name + " added successfully.",
                        Type = NotificationTypes.Success,
                    });
                }

            }
            else
            {
                notifications.Add(new Notification()
                {
                    Title = "Error !",
                    Content = "No team found with this code",
                    Type = NotificationTypes.Error,
                });
            }

            TempData["Notifications"] = notifications;
            return RedirectToAction("Details", new { id = projectID });
        }

        public ActionResult RemoveTeam(int projectID, int teamID)
        {
            Project project = db.Projects.Find(projectID);
            Team team = db.Teams.Find(teamID);

            project.Teams.Remove(team);

            db.SaveChanges();

            List<Notification> notifications = new List<Notification>();

            notifications.Add(new Notification()
            {
                Title = "Success !",
                Content = "Team: " + team.Name + " removed successfully.",
                Type = NotificationTypes.Success,
            });

            TempData["Notifications"] = notifications;

            return RedirectToAction("Details", new { id = projectID });
        }
    }

}