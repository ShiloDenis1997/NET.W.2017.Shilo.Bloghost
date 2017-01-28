using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces.Managers;
using ORM;

namespace DAL.Concrete
{
    public class LikeManager : ILikeManager
    {
        private DbContext context;

        public LikeManager(DbContext context)
        {
            this.context = context;
        }

        public bool CreateArticleLike(int articleId, int userId)
        {
            var article = context.Set<Article>().Find(articleId);
            if (article == null)
                throw new ArgumentException
                    ($"Article with id = {articleId} does no exist");
            var user = context.Set<User>().Find(userId);
            if (user == null)
                throw new ArgumentException
                    ($"User with id = {userId} does no exist");
            if (article.LikedUsers.Contains(user))
                return false;
            article.LikedUsers.Add(user);
            article.Rating++;
            context.Entry(article).State = EntityState.Modified;
            return true;
        }

        public bool CreateBlogLike(int blogId, int userId)
        {
            var blog = context.Set<Blog>().Find(blogId);
            if (blog == null)
                throw new ArgumentException
                    ($"Blog with id = {blogId} does no exist");
            var user = context.Set<User>().Find(userId);
            if (user == null)
                throw new ArgumentException
                    ($"User with id = {userId} does no exist");
            if (blog.LikedUsers.Contains(user))
                return false;
            blog.LikedUsers.Add(user);
            blog.Rating++;
            context.Entry(blog).State = EntityState.Modified;
            return true;
        }

        public bool CreateCommentLike(int commentId, int userId)
        {
            var comment = context.Set<Comment>().Find(commentId);
            if (comment == null)
                throw new ArgumentException
                    ($"Comment with id = {commentId} does no exist");
            var user = context.Set<User>().Find(userId);
            if (user == null)
                throw new ArgumentException
                    ($"User with id = {userId} does no exist");
            if (comment.LikedUsers.Contains(user))
                return false;
            comment.LikedUsers.Add(user);
            comment.Rating++;
            context.Entry(comment).State = EntityState.Modified;
            return true;
        }

        public bool DeleteArticleLike(int articleId, int userId)
        {
            var article = context.Set<Article>().Find(articleId);
            if (article == null)
                throw new ArgumentException
                    ($"Article with id = {articleId} does no exist");
            var user = context.Set<User>().Find(userId);
            if (user == null)
                throw new ArgumentException
                    ($"User with id = {userId} does no exist");
            if (!article.LikedUsers.Contains(user))
                return false;
            article.LikedUsers.Remove(user);
            article.Rating--;
            context.Entry(article).State = EntityState.Modified;
            return true;
        }

        public bool DeleteBlogLike(int blogId, int userId)
        {
            var blog = context.Set<Blog>().Find(blogId);
            if (blog == null)
                throw new ArgumentException
                    ($"Blog with id = {blogId} does no exist");
            var user = context.Set<User>().Find(userId);
            if (user == null)
                throw new ArgumentException
                    ($"User with id = {userId} does no exist");
            if (!blog.LikedUsers.Contains(user))
                return false;
            blog.LikedUsers.Remove(user);
            blog.Rating--;
            context.Entry(blog).State = EntityState.Modified;
            return true;
        }

        public bool DeleteCommentLike(int commentId, int userId)
        {
            var comment = context.Set<Comment>().Find(commentId);
            if (comment == null)
                throw new ArgumentException
                    ($"Comment with id = {commentId} does no exist");
            var user = context.Set<User>().Find(userId);
            if (user == null)
                throw new ArgumentException
                    ($"User with id = {userId} does no exist");
            if (!comment.LikedUsers.Contains(user))
                return false;
            comment.LikedUsers.Remove(user);
            comment.Rating--;
            context.Entry(comment).State = EntityState.Modified;
            return true;
        }
    }
}
