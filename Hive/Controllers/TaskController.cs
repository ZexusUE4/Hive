using Hive.DAL;
using Hive.Helpers;
using Hive.Models;
using Hive.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSP1260.Controllers
{
    public class TaskController : Controller
    {
        private HiveContext db = new HiveContext();


        public TaskController()
        {

        }
        public TaskController(HiveContext db)
        {
            this.db = db;
        }

        public ActionResult Create(int projectID)
        {
            TaskCreateVM model = new TaskCreateVM();
            model.Task = new Task()
            {
                DateAssigned = DateTime.Now,
                Status = TaskStatuses.Active,
                ProjectID = projectID,
                Deadline = DateTime.Now.AddDays(1),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskCreateVM model)
        {
            if (ModelState.IsValid)
            {

                model.SelectedAssignees.ForEach(id =>
                {
                    Team team = db.Teams.Find(int.Parse(id));
                    model.Task.Assignees.Add(team);
                });

                db.Tasks.Add(model.Task);
                db.SaveChanges();

                model.Task.Assignees.ToList().ForEach(a =>
                {
                    EmailNotifier.SendTaskAssignedToTeam(model.Task.ID, a.ID);
                });

                List<Notification> notifications = new List<Notification>();

                notifications.Add(new Notification()
                {
                    Title = "Success !",
                    Content = "Task: " + model.Task.Title + " added successfully.",
                    Type = NotificationTypes.Success,
                });

                TempData["Notifications"] = notifications;

                return RedirectToAction("Details","Project", new { id = model.Task.ProjectID });
            }

            return View(model);
        }

        public ActionResult ChangeStatus(int id, TaskStatuses status)
        {
            Task task = db.Tasks.Find(id);

            task.Status = status;

            db.SaveChanges();

            return RedirectToAction("Details", "Project", new { id = task.ProjectID });
        }

        public ActionResult Details(int id)
        {
            Task task = db.Tasks.Find(id);

            if (task == null)
            {
                return HttpNotFound();
            }

            return View(task);

        }

        public ActionResult Edit(int id)
        {
            Task task = db.Tasks.Find(id);

            if (task == null)
            {
                return HttpNotFound();
            }

            TaskCreateVM model = new TaskCreateVM();

            model.Task = task;

            model.Task.Assignees.ToList().ForEach(a =>
            {
                model.SelectedAssignees.Add(a.ID.ToString());
            });

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskCreateVM model)
        {
            if (ModelState.IsValid)
            {
                Task inDb = db.Tasks.Find(model.Task.ID);

                inDb.Assignees.Clear();

                model.SelectedAssignees.ForEach(id =>
                {
                    Team team = db.Teams.Find(int.Parse(id));
                    inDb.Assignees.Add(team);
                });

                db.Entry(inDb).CurrentValues.SetValues(model.Task);
                db.SaveChanges();

                return RedirectToAction("Details", "Project", new { id = inDb.ProjectID });
            }

            return View(model);

        }

        public ActionResult Delete(int id)
        {
            Task task = db.Tasks.Find(id);

            if (task == null)
            {
                return HttpNotFound();
            }

            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);

            int projectID = task.ProjectID;

            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Details", "Project", new { id = projectID });
        }
    }
}