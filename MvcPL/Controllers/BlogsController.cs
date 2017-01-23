using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPL.Models;
using ORM;

namespace MvcPL.Controllers
{
    public class BlogsController : Controller
    {
        private DbContext context;

        public BlogsController(DbContext context)
        {
            this.context = context;
        }

        // GET: Blogs
        public ActionResult Index(int? userId)
        {
            IEnumerable<Blog> blogs = context.Set<Blog>().Include(blog => blog.User);
            if (userId != null)
                blogs = blogs.Where(user => user.Id == userId);

            return View(blogs.Select(blog => new BlogViewModel
            {
                Id = blog.Id,
                DateStarted = blog.DateStarted,
                Name = blog.Name,
                UserId = blog.UserId,
                UserName = blog.User.Login,
                Rating = blog.Rating,
            }));
        }
    }
}