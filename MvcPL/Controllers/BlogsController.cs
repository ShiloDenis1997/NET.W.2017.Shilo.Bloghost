﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
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

        public BlogsController
            (IBlogService blogService, IUserService userService)
        {
            this.blogService = blogService;
            this.userService = userService;
        }

        // GET: Blogs
        public ActionResult Index(int? userId)
        {
            IEnumerable<BlogEntity> blogs;

            if (userId != null)
            {
                blogs = blogService.GetBlogsByPredicate
                            (blog => blog.UserId == userId, 100)
                            .OrderByDescending(blog => blog.DateStarted);
            }
            else
            {
                blogs = blogService.GetBlogsByCreationDate(100, 0, false);
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
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(userService.GetUserEntities(50), "Id", "Login");
            return View();
        }

        // POST: Blogs/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Rating,DateStarted,UserId")] BlogViewModel blog)
        {
            if (ModelState.IsValid)
            {
                blogService.CreateBlog(blog.ToBllBlog());
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(userService.GetUserEntities(50), "Id", "Login");
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
            ViewBag.UserId = new SelectList(userService.GetUserEntities(50), "Id", "Login");
            var user = userService.GetUserEntity(blog.UserId);
            var viewBlog = blog.ToMvcBlog(user.Login);
            return View(viewBlog);
        }

        // POST: Blogs/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Rating,DateStarted,UserId")] BlogViewModel blog)
        {
            if (ModelState.IsValid)
            {
                blogService.UpdateBlog(blog.ToBllBlog());
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(userService.GetUserEntities(50), "Id", "Login");
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