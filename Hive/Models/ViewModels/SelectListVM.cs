using Hive.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hive.Models.ViewModels
{
    public class SelectListVM
    {
        private static HiveContext db = new HiveContext();

        public static IEnumerable<SelectListItem> GetProjectAssignees(int projectID)
        {
            Project project = db.Projects.Find(projectID);

            return new SelectList(project.Teams, "ID", "Name");
        }
    }
}