using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.Managers
{
    public interface ILikeManager
    {
        bool CreateBlogLike(int blogId, int userId);
        bool CreateArticleLike(int articleId, int userId);
        bool CreateCommentLike(int commentId, int userId);
        bool DeleteBlogLike(int blogId, int userId);
        bool DeleteArticleLike(int articleId, int userId);
        bool DeleteCommentLike(int commentId, int userId);
    }
}
