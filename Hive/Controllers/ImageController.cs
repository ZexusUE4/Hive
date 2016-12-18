using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hive.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult GetImageView(string fileName)
        {
            if (System.IO.File.Exists(fileName))
                return File(System.IO.File.ReadAllBytes(fileName), "image/jpg");
            else
                return File(System.IO.File.ReadAllBytes(Server.MapPath("~/App_Data/ProfilePics/man.png")), "image/png");
        }
    }
}