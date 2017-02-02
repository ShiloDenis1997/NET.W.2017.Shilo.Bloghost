using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Interfaces.Entities;
using MvcPL.Models.Entities;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcMapper
    {
        public static BlogEntity ToBllBlog(this BlogViewModel blog)
        {
            return new BlogEntity
            {
                Id = blog.Id,
                Name = blog.Name,
                Rating = blog.Rating ?? 0,
                UserId = blog.UserId,
                DateStarted = blog.DateStarted,
            };
        }

        public static BlogViewModel ToMvcBlog
            (this BlogEntity blog, string userName, string userEmail)
        {
            return new BlogViewModel
            {
                Id = blog.Id,
                Name = blog.Name,
                Rating = blog.Rating,
                UserId = blog.UserId,
                DateStarted = blog.DateStarted,
                UserName = userName,
                UserEmail = userEmail,
            };
        }

        public static ArticleViewModel ToMvcArticle
            (this ArticleEntity article, string blogName, 
                string userEmail, string userName)
        {
            return new ArticleViewModel
            {
                Id = article.Id,
                Name = article.Name,
                Rating = article.Rating,
                DateAdded = article.DateAdded,
                BlogId = article.BlogId,
                Content = article.Content,
                UserId = article.UserId,
                UserName = userName,
                BlogName = blogName,
                UserEmail = userEmail,
                Tags = article.Tags.ToArray(),
            };
        }

        public static ArticleEntity ToBllArticle
            (this ArticleViewModel article)
        {
            return new ArticleEntity
            {
                Id = article.Id,
                Name = article.Name,
                Rating = article.Rating ?? 0,
                UserId = article.UserId ?? 0,
                BlogId = article.BlogId,
                DateAdded = article.DateAdded,
                Content = article.Content,
                Tags = article.Tags,
            };
        }

        public static CommentEntity ToBllComment
            (this CommentViewModel comment)
        {
            return new CommentEntity
            {
                Id = comment.Id,
                Rating = comment.Rating ?? 0,
                UserId = comment.UserId,
                Content = comment.Content,
                DateAdded = comment.DateAdded,
                ArticleId = comment.ArticleId,
            };
        }

        public static CommentViewModel ToMvcComment
            (this CommentEntity comment, string userName = null)
        {
            return new CommentViewModel
            {
                Id = comment.Id,
                Rating = comment.Rating,
                UserId = comment.UserId,
                Content = comment.Content,
                DateAdded = comment.DateAdded,
                ArticleId = comment.ArticleId,
                UserName = userName,
            };
        }
    }
}