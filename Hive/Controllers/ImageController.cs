using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSP1260.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult GetImageView(string fileName)
        {
            if (System.IO.File.Exists(fileName))
                return File(System.IO.File.ReadAllBytes(fileName), "image/jpg");
            else
                return File(System.IO.File.ReadAllBytes(Server.MapPath("~/AppData/ProfilePics/man.png")), "image/png");
        }
    }
}