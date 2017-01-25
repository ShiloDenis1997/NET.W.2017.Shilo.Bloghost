using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Services;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    public class ArticlesController : Controller
    {
        private IArticleService articleService;
        private IUserService userService;

        public ArticlesController
            (IArticleService articleService, IUserService userService)
        {
            this.articleService = articleService;
            this.userService = userService;
        }

        // GET: Articles
        public ActionResult Index()
        {
            IEnumerable<ArticleEntity> articles = articleService
                .GetArticlesByCreationDate(100);
            return View(articles.Select(
                article =>
                {
                    var user = userService.GetUserEntity(article.UserId);
                    return new ArticleViewModel
                    {
                        Id = article.Id,
                        AuthorId = user.Id,
                        AuthorName = user.Login,
                        BlogId = article.BlogId,
                        Content = article.Content,
                        DateAdded = article.DateAdded,
                        Name = article.Name,
                        Rating = article.Rating,
                    };
                }
            ).ToArray());
        }
    }
}
