using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Mvc;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    public class BlogsController : Controller
    {
        private IBlogService blogService;
        private IUserService userService;
        private ILikeService likeService;

        private const int PageBlogsCount = 100;

        public BlogsController
            (IBlogService blogService, IUserService userService,
            ILikeService likeService)
        {
            this.blogService = blogService;
            this.userService = userService;
            this.likeService = likeService;
        }

        // GET: Blogs
        public ActionResult Index(int? userId)
        {
            IEnumerable<BlogEntity> blogs;

            if (userId != null)
            {
                blogs = blogService.GetBlogsByPredicate
                            (blog => blog.UserId == userId, PageBlogsCount)
                            .OrderByDescending(blog => blog.DateStarted);
            }
            else
            {
                blogs = blogService.GetBlogsByCreationDate(PageBlogsCount);
            }

            return View(blogs.Select(blog => new BlogViewModel
            {
                Id = blog.Id,
                DateStarted = blog.DateStarted,
                Name = blog.Name,
                UserId = blog.UserId,
                UserName = userService.GetUserEntity(blog.UserId).Login,
                Rating = blog.Rating,
            }).ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult Like(int blogId)
        {
            var user = userService.GetUserByPredicate(u => u.Email.Equals(User.Identity.Name));
            if (!likeService.LikeBlog(blogId, user.Id))
                likeService.RemoveLikeBlog(blogId, user.Id);
            return RedirectToAction("Index");
        }

        // GET: Blogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogEntity blog = blogService.GetBlogEntity(id.Value);

            if (blog == null)
            {
                return HttpNotFound();
            }
            var user = userService.GetUserEntity(blog.UserId);
            var viewBlog = blog.ToMvcBlog(user.Login);
            return View(viewBlog);
        }

        // GET: Blogs/Create
        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blogs/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult Create(BlogViewModel blog)
        {
            if (ModelState.IsValid)
            {
                var user = userService.GetUserByPredicate
                    (u => u.Email.Equals(User.Identity.Name));
                var bllBlog = blog.ToBllBlog();
                bllBlog.UserId = user.Id;
                bllBlog.DateStarted = DateTime.Now;
                blogService.CreateBlog(bllBlog);
                return RedirectToAction("Index");
            }
            
            return View(blog);
        }

        // GET: Blogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogEntity blog = blogService.GetBlogEntity(id.Value);
            if (blog == null)
            {
                return HttpNotFound();
            }
            var viewBlog = blog.ToMvcBlog(null);
            return View(viewBlog);
        }

        // POST: Blogs/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BlogViewModel blog)
        {
            if (ModelState.IsValid)
            {
                blogService.UpdateBlog(blog.ToBllBlog());
                return RedirectToAction("Index");
            }
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogEntity blog = blogService.GetBlogEntity(id.Value);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog.ToMvcBlog(null));
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            blogService.DeleteBlog(new BlogEntity {Id = id});
            return RedirectToAction("Index");
        }
    }
}