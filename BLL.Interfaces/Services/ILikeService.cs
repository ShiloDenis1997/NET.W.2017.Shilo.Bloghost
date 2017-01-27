using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Services
{
    public interface ILikeService
    {
        bool LikeBlog(int blogId, int userId);
        bool LikeArticle(int articleId, int userId);
        bool LikeComment(int commentId, int userId);
        bool RemoveLikeBlog(int blogId, int userId);
        bool RemoveLikeArticle(int articleId, int userId);
        bool RemoveLikeComment(int commentId, int userId);
    }
}
