using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.Entities;
using DAL.Interfaces.DTO;

namespace BLL.Concrete.Mappers
{
    public static class BllMappers
    {
        public static DalUser ToDalUser(this UserEntity user)
        {
            return new DalUser
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                Firstname = user.Firstname,
                Secondname = user.Secondname,
                Thirdname = user.Thirdname,
                DateRegistered = user.DateRegistered,
                Email = user.Email,
                Roles = user.Roles.ToArray()
            };
        }

        public static UserEntity ToUserEntity(this DalUser user)
        {
            return new UserEntity
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                Firstname = user.Firstname,
                Secondname = user.Secondname,
                Thirdname = user.Thirdname,
                DateRegistered = user.DateRegistered,
                Email = user.Email,
                Roles = user.Roles.ToArray()
            };
        }

        public static DalRole ToDalRole(this RoleEntity role)
        {
            return new DalRole
            {
                Id = role.Id,
                Rolename = role.Rolename,
            };
        }

        public static RoleEntity ToRoleEntity(this DalRole role)
        {
            return new RoleEntity
            {
                Id = role.Id,
                Rolename = role.Rolename
            };
        }

        public static BlogEntity ToBlogEntity(this DalBlog blog)
        {
            return new BlogEntity
            {
                Id = blog.Id,
                Name = blog.Name,
                Rating = blog.Rating,
                UserId = blog.UserId,
                DateStarted = blog.DateStarted
            };
        }

        public static DalBlog ToDalBlog(this BlogEntity blog)
        {
            return new DalBlog
            {
                Id = blog.Id,
                Name = blog.Name,
                Rating = blog.Rating,
                UserId = blog.UserId,
                DateStarted = blog.DateStarted
            };
        }

        public static DalArticle ToDalArticle(this ArticleEntity article)
        {
            return new DalArticle
            {
                Id = article.Id,
                Name = article.Name,
                Rating = article.Rating,
                UserId = article.UserId,
                DateAdded = article.DateAdded,
                BlogId = article.BlogId,
                Content = article.Content,
            };
        }

        public static ArticleEntity ToBllArticle(this DalArticle article)
        {
            return new ArticleEntity
            {
                Id = article.Id,
                Name = article.Name,
                Rating = article.Rating,
                UserId = article.UserId,
                DateAdded = article.DateAdded,
                BlogId = article.BlogId,
                Content = article.Content,
            };
        }

        public static CommentEntity ToBllComment(this DalComment comment)
        {
            return new CommentEntity
            {
                Id = comment.Id,
                Rating = comment.Rating,
                UserId = comment.UserId,
                Content = comment.Content,
                DateAdded = comment.DateAdded,
                ArticleId = comment.ArticleId,
            };
        }

        public static DalComment ToDalComment(this CommentEntity comment)
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
    }
}
