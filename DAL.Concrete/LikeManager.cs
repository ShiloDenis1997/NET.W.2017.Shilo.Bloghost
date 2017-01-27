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
            throw new NotImplementedException();
        }

        public bool CreateCommentLike(int commentId, int userId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteArticleLike(int articleId, int userId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBlogLike(int blogId, int userId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCommentLike(int commentId, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
