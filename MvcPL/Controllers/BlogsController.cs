﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Services;
using MvcPL.Models;
using ORM;

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
            }).ToArray());
        }
    }
}