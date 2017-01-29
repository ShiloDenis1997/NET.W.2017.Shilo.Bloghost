using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces.Services;

namespace MvcPL.Controllers
{
    public class NavController : Controller
    {
        private IUserService userService;

        public NavController(IUserService userService)
        {
            this.userService = userService;
        }

        // GET: Nav
        public ActionResult Menu()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = userService.GetUserByPredicate(u => u.Email.Equals(User.Identity.Name));
                return PartialView(user.Id);
            }
            return PartialView();
        }
    }
}