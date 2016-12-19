using Hive.DAL;
using Hive.Helpers;
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


        public TeamController()
        {

        }

        public TeamController(HiveContext db)
        {
            this.db = db;
        }


        // GET: Team
        public ActionResult Index()
        {

            HiveUser user = db.Users.Find(User.Identity.GetUserId());

            return View(user.Teams);
        }

        public ActionResult Create()
        {
            Team team = new Team();
            team.Code = Team.GetUniqueCode(db);
            team.DateCreated = DateTime.Now;

            return View(team);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Team model)
        {
            if (ModelState.IsValid)
            {
                TeamMember leaderMember = new TeamMember()
                {
                    Team = model,
                    MemberID = User.Identity.GetUserId(),
                    IsLeader = true,
                    Status = TeamMemberStatuses.Accepted,
                };

                model.TeamMembers.Add(leaderMember);
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

        public ActionResult InviteMember(int teamID, string email)
        {
            Team team = db.Teams.Find(teamID);
            HiveUser user = db.Users.FirstOrDefault(u => u.Email == email);

            EmailNotifier.SendTeamInvitation(teamID, email);

            if (user != null)
            {
                TeamMember member = new TeamMember()
                {
                    Member = user,
                    Team = team,
                    Status = TeamMemberStatuses.Pending,
                    IsLeader = false,
                };

                team.TeamMembers.Add(member);
                db.SaveChanges();
            }
            else
            {

            }

            return RedirectToAction("Details", new { id = team.ID });
        }

        public ActionResult AcceptInvitation(int teamID)
        {
            Team team = db.Teams.Find(teamID);

            TeamMember member = team.TeamMembers.FirstOrDefault(m => m.MemberID == User.Identity.GetUserId());

            if(member == null)
            {
                member = new TeamMember()
                {
                    MemberID = User.Identity.GetUserId(),
                    Team = team,
                    Status = TeamMemberStatuses.Accepted,
                    DateJoined = DateTime.Now,
                    IsLeader = false,
                };

                team.TeamMembers.Add(member);
            }
            else
            {
                if(member.Status == TeamMemberStatuses.Accepted)
                {
                    return HttpNotFound("You are already in this team");
                }

                member.Status = TeamMemberStatuses.Accepted;
                member.DateJoined = DateTime.Now;
            }

            db.SaveChanges();

            return RedirectToAction("Details", new { id = team.ID });
        }

        public ActionResult RemoveMember(int memberID)
        {
            TeamMember member = db.TeamMembers.Find(memberID);

            int teamID = member.TeamID;

            db.TeamMembers.Remove(member);

            db.SaveChanges();

            Team team = db.Teams.Find(teamID);

            if(team.IsMemberLoggedIn())
            {
                return RedirectToAction("Details", new { id = teamID });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
