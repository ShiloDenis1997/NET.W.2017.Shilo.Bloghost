using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.Services;
using DAL.Interfaces.Managers;

namespace BLL.Concrete
{
    public class LikeService : ILikeService
    {
        private ILikeManager likeManager;

        public LikeService(ILikeManager likeManager)
        {
            this.likeManager = likeManager;
        }

        public bool LikeArticle(int articleId, int userId)
        {
            return likeManager.CreateArticleLike(articleId, userId);
        }

        public bool LikeBlog(int blogId, int userId)
        {
            throw new NotImplementedException();
        }

        public bool LikeComment(int commentId, int userId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveLikeArticle(int articleId, int userId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveLikeBlog(int blogId, int userId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveLikeComment(int commentId, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
