using Hive.DAL;
using Hive.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSP1260.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private HiveContext db = new HiveContext();

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
                model.Color = Project.GetRandomColor();

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
                db.Entry(model).State = EntityState.Modified;
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
    }

}