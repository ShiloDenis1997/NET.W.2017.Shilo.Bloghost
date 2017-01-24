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
    public class ArticlesController : Controller
    {
        private DbContext context;

        public ArticlesController(DbContext context)
        {
            this.context = context;
        }

        // GET: Articles
        public ActionResult Index()
        {
            IEnumerable<Article> articles = context.Set<Article>()
                .Include(article => article.Blog.User);
            return View(articles.Select(
                article => new ArticleViewModel
                {
                    Id = article.Id,
                    AuthorId = article.Blog.User.Id,
                    AuthorName = article.Blog.User.Login,
                    BlogId = article.BlogId,
                    Content = article.Content,
                    DateAdded = article.DateAdded,
                    Name = article.Name,
                    Rating = article.Rating,
                }
            ).ToArray());
        }
    }
}
