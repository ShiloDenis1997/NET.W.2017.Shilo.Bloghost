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
using MvcPL.Models.Entities;
using MvcPL.Models.Lists;

namespace MvcPL.Controllers
{
    public class ArticlesController : Controller
    {
        private IArticleService articleService;
        private IUserService userService;
        private IBlogService blogService;
        private ILikeService likeService;

        private const int PageArticlesCount = 4;

        public ArticlesController
            (IArticleService articleService, IUserService userService,
                IBlogService blogService, ILikeService likeService)
        {
            this.articleService = articleService;
            this.userService = userService;
            this.blogService = blogService;
            this.likeService = likeService;
        }

        // GET: Articles
        public ActionResult Index
            (int page = 1, int? userId = null, int? blogId = null, 
                string tag = null, string text = null)
        {
            IEnumerable<ArticleEntity> articles;
            if (blogId != null)
            {
                articles = articleService.GetArticlesByPredicate(
                    article => article.BlogId == blogId.Value, PageArticlesCount,
                    PageArticlesCount * (page - 1));
            }
            else if (userId != null)
            {
                articles = articleService.GetArticlesByUser
                    (userId.Value, PageArticlesCount, PageArticlesCount * (page - 1));
            }
            else if (!string.IsNullOrWhiteSpace(tag))
            {
                articles = articleService.GetArticlesByTag
                    (tag, PageArticlesCount, PageArticlesCount * (page - 1));
            }
            else if (!string.IsNullOrWhiteSpace(text))
            {
                articles = articleService.GetArticlesWithText
                    (text, PageArticlesCount, PageArticlesCount * (page - 1));
            }
            else
            {
                articles = articleService.GetArticlesByCreationDate
                    (PageArticlesCount, PageArticlesCount * (page - 1));
            }

            return View(new ArticlesListViewModel
            {
                PagingInfo = new PagingInfo
                {
                    CurrentItemsCount = articles?.Count() ?? 0,
                    ItemsPerPage = PageArticlesCount,
                    CurrentPage = page,
                },
                Articles = articles?.Select(
                    article =>
                    {
                        var user = userService.GetUserEntity(article.UserId);
                        return article.ToMvcArticle(null, user.Login);
                    }
                ).ToArray() ?? new ArticleViewModel[] {},
                UserId = userId,
                BlogId = blogId,
                Tag = tag,
                Text = text,
            });
        }

        public ActionResult Popular(int page = 1)
        {
            var articles = articleService.GetArticlesByPopularity(
                PageArticlesCount, PageArticlesCount*(page - 1));
            return View(new ArticlesListViewModel
            {
                Articles = articles.Select(
                    article =>
                    {
                        var user = userService.GetUserEntity(article.UserId);
                        return article.ToMvcArticle(null, user.Login);
                    }),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    CurrentItemsCount = articles.Count(),
                    ItemsPerPage = PageArticlesCount,
                },
            });
        }

        [Authorize(Roles = "User")]
        public ActionResult Like(int articleId, string likeUrl)
        {
            var user = userService.GetUserByPredicate(u => u.Email.Equals(User.Identity.Name));
            if (!likeService.LikeArticle(articleId, user.Id))
                likeService.RemoveLikeArticle(articleId, user.Id);
            if (Url.IsLocalUrl(likeUrl))
                return Redirect(likeUrl);
            return RedirectToAction("Index");
        }

        
        // GET: Articles/Details/5
        public ActionResult Details(int? id, int commentPackNumber = 1)
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
            ViewBag.CommentPackNumber = commentPackNumber;
            return View(article.ToMvcArticle(blog.Name, user.Login));
        }

        // GET: Articles/Create
        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            UserEntity currentUser = userService.GetUserByPredicate(
                user => user.Email.Equals(User.Identity.Name));
            ViewBag.BlogId = new SelectList
                (blogService.GetBlogsByPredicate
                    (blog => blog.UserId == currentUser.Id, int.MaxValue), "Id", "Name");
            return View();
        }

        // POST: Articles/Create
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
            UserEntity currentUser = userService.GetUserByPredicate(
                user => user.Email.Equals(User.Identity.Name));
            ViewBag.BlogId = new SelectList
                (blogService.GetBlogsByPredicate
                    (blog => blog.UserId == currentUser.Id, int.MaxValue), "Id", "Name");
            return View(article);
        }

        // GET: Articles/Edit/5
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
                    (blog => blog.UserId == currentUser.Id, int.MaxValue), "Id", "Name");
            return View(article.ToMvcArticle());
        }

        // POST: Articles/Edit/5
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
                    (blog => blog.UserId == currentUser.Id, int.MaxValue), "Id", "Name");
            return View(article);
        }

        // GET: Articles/Delete/5
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
            BlogEntity blog = blogService.GetBlogEntity(article.BlogId);
            UserEntity user = userService.GetUserEntity(article.UserId);
            return View(article.ToMvcArticle(blog.Name, user.Login));
        }

        // POST: Articles/Delete/5
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
