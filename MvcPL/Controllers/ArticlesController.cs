using System;
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
    public class ArticlesController : Controller
    {
        private IArticleService articleService;
        private IUserService userService;
        private IBlogService blogService;

        private const int PageArticlesCount = 100;

        public ArticlesController
            (IArticleService articleService, IUserService userService,
                IBlogService blogService)
        {
            this.articleService = articleService;
            this.userService = userService;
            this.blogService = blogService;
        }

        // GET: Articles
        public ActionResult Index(int? userId, int? blogId)
        {
            IEnumerable<ArticleEntity> articles;
            if (blogId != null)
            {
                articles = articleService.GetArticlesByPredicate(
                    article => article.BlogId == blogId.Value, PageArticlesCount);
            }
            else if (userId != null)
            {
                articles = articleService.GetArticlesByUser
                    (userId.Value, PageArticlesCount);
            }
            else
            {
                articles = articleService.GetArticleEntities(PageArticlesCount);
            }

            return View(articles.Select(
                article =>
                {
                    var user = userService.GetUserEntity(article.UserId);
                    return article.ToMvcArticle(null, user.Login);
                }
            ).ToArray());
        }

        // GET: ArticlesEdit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleEntity article = articleService.GetArticleEntity(id.Value);
            if (article == null)
            {
                return HttpNotFound();
            }
            BlogEntity blog = blogService.GetBlogEntity(article.BlogId);
            UserEntity user = userService.GetUserEntity(article.UserId);
            return View(article.ToMvcArticle(blog.Name, user.Login));
        }

        // GET: ArticlesEdit/Create
        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            UserEntity currentUser = userService.GetUserByPredicate(
                user => user.Email.Equals(User.Identity.Name));
            ViewBag.BlogId = new SelectList
                (blogService.GetBlogsByPredicate
                    (blog => blog.UserId == currentUser.Id, PageArticlesCount), "Id", "Name");
            return View();
        }

        // POST: ArticlesEdit/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "User")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleViewModel article)
        {
            if (ModelState.IsValid)
            {
                article.DateAdded = DateTime.Now;
                articleService.CreateArticle(article.ToBllArticle());
                return RedirectToAction("Index");
            }

            ViewBag.BlogId = new SelectList
                (blogService.GetBlogEntities(PageArticlesCount), "Id", "Name", article.BlogId);
            return View(article);
        }

        // GET: ArticlesEdit/Edit/5
        [Authorize(Roles = "User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleEntity article = articleService.GetArticleEntity(id.Value);
            if (article == null)
            {
                return HttpNotFound();
            }
            UserEntity currentUser = userService.GetUserByPredicate(
                user => user.Email.Equals(User.Identity.Name));
            ViewBag.BlogId = new SelectList
                (blogService.GetBlogsByPredicate
                    (blog => blog.UserId == currentUser.Id, PageArticlesCount), "Id", "Name");
            return View(article.ToMvcArticle());
        }

        // POST: ArticlesEdit/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "User")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArticleViewModel article)
        {
            if (ModelState.IsValid)
            {
                articleService.UpdateArticle(article.ToBllArticle());
                return RedirectToAction("Index");
            }
            UserEntity currentUser = userService.GetUserByPredicate(
                user => user.Email.Equals(User.Identity.Name));
            ViewBag.BlogId = new SelectList
                (blogService.GetBlogsByPredicate
                    (blog => blog.UserId == currentUser.Id, PageArticlesCount), "Id", "Name");
            return View(article);
        }

        // GET: ArticlesEdit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleEntity article = articleService.GetArticleEntity(id.Value);
            if (article == null)
            {
                return HttpNotFound();
            }
            BlogEntity blog = blogService.GetBlogEntity(id.Value);
            UserEntity user = userService.GetUserEntity(id.Value);
            return View(article.ToMvcArticle(blog.Name, user.Login));
        }

        // POST: ArticlesEdit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArticleEntity article = articleService.GetArticleEntity(id);
            articleService.DeleteArticle(article);
            return RedirectToAction("Index");
        }
    }
}
