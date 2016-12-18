using Hive.DAL;
using Hive.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hive.Controllers
{
    public class TeamController : Controller
    {
        private HiveContext db = new HiveContext();

        // GET: Team
        public ActionResult Index()
        {

            HiveUser user = db.Users.Find(User.Identity.GetUserId());

            return View(user.Teams);
        }

        public ActionResult Create()
        {
            Team team = new Team();

       // team.TeamLeader.Id= User.Identity.GetUserId();
            team.DateCreated = DateTime.Now;

            return View(team);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Team model)
        {
            if (ModelState.IsValid)
            {
                db.Teams.Add(model);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = model.ID });
            }

            return View(model);
        }
        public ActionResult Details(int id)
        {
            Team team = db.Teams.Find(id);

            if (team == null)
            {
                return HttpNotFound();
            }

            return View(team);
        }
        public ActionResult Edit(int id)
        {
            Team team = db.Teams.Find(id);

            if (team == null)
            {
                return HttpNotFound();
            }

            return View(team);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Team model)
        {
            if (ModelState.IsValid)
            {
                Team inDb = db.Teams.Find(model.ID);

                db.Entry(inDb).CurrentValues.SetValues(model);
                db.SaveChanges();

                return RedirectToAction("Details", new { id = model.ID });
            }

            return View(model);

        }

        public ActionResult Delete(int id)
        {
            Team team = db.Teams.Find(id);

            if (team == null)
            {
                return HttpNotFound();
            }

            return View(team);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //missing : delete team only by TeamLeader
            Team team = db.Teams.Find(id);
            db.Teams.Remove(team);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
