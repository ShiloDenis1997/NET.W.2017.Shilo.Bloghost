using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ILogger = Logger.Interfaces.ILogger;
using Logger.Concrete;

namespace MvcPL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ILogger logger;

        public HomeController(ILogger logger)
        {
            this.logger = logger;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            logger.Trace("Index action of Home controller started");
            return View();
        }
    }
}