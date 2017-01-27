using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces.DTO;
using ORM;

namespace DAL.Concrete.Mappers
{
    public static class DalMappers
    {
        public static User ToOrmUser(this DalUser user)
        {
            return new User
            {
                Id = user.Id,
                Login = user.Login,
                Firstname = user.Firstname,
                Secondname = user.Secondname,
                Thirdname = user.Thirdname,
                Email = user.Email,
                Password = user.Password,
                DateRegistered = user.DateRegistered,
            };
        }

        public static DalUser ToDalUser(this User user)
        {
            return new DalUser
            {
                Id = user.Id,
                Login = user.Login,
                Firstname = user.Firstname,
                Secondname = user.Secondname,
                Thirdname = user.Thirdname,
                Email = user.Email,
                Password = user.Password,
                DateRegistered = user.DateRegistered,
                Roles = user.Roles.Select(role=>role.Rolename).ToArray(),
            };
        }

        public static Blog ToOrmBlog(this DalBlog blog)
        {
            return new Blog
            {
                Id = blog.Id,
                UserId = blog.UserId,
                Rating = blog.Rating,
                DateStarted = blog.DateStarted,
                Name = blog.Name,
            };
        }

        public static DalBlog ToDalBlog(this Blog blog)
        {
            return new DalBlog
            {
                Id = blog.Id,
                Name = blog.Name,
                Rating = blog.Rating,
                UserId = blog.UserId,
                DateStarted = blog.DateStarted,
            };
        }

        public static Article ToOrmArticle(this DalArticle article)
        {
            return new Article
            {
                BlogId = article.BlogId,
                Content = article.Content,
                DateAdded = article.DateAdded,
                Id = article.Id,
                Name = article.Name,
                Rating = article.Rating,
            };
        }

        public static DalArticle ToDalArticle(this Article article)
        {
            return new DalArticle
            {
                Id = article.Id,
                BlogId = article.BlogId,
                Name = article.Name,
                Rating = article.Rating,
                UserId = article.Blog.UserId,
                DateAdded = article.DateAdded,
                Content = article.Content,
                Tags = article.Tags.Select(tag => tag.Name).ToArray(),
            };
        }

        public static DalComment ToDalComment(this Comment comment)
        {
            return new DalComment
            {
                Id = comment.Id,
                Rating = comment.Rating,
                UserId = comment.UserId,
                Content = comment.Content,
                DateAdded = comment.DateAdded,
                ArticleId = comment.ArticleId,
            };
        }

        public static Comment ToOrmComment(this DalComment comment)
        {
            return new Comment
            {
                Id = comment.Id,
                Rating = comment.Rating,
                UserId = comment.UserId,
                Content = comment.Content,
                DateAdded = comment.DateAdded,
                ArticleId = comment.ArticleId,
            };
        }
    }
}
