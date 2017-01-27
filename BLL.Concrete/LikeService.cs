using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.Services;
using DAL.Interfaces.Managers;
using DAL.Interfaces.Repository;

namespace BLL.Concrete
{
    public class LikeService : ILikeService
    {
        private ILikeManager likeManager;
        private IUnitOfWork unitOfWork;

        public LikeService(ILikeManager likeManager, IUnitOfWork unitOfWork)
        {
            this.likeManager = likeManager;
            this.unitOfWork = unitOfWork;
        }

        public bool LikeArticle(int articleId, int userId)
        {
            bool result = likeManager.CreateArticleLike(articleId, userId);
            unitOfWork.Commit();
            return result;
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
