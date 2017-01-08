using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ORM;

namespace MvcPL.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            BlogHostModel db = new BlogHostModel();
            User user = db.Users.FirstOrDefault();
            ViewBag.User = user;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}