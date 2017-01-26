using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.Entities;

namespace BLL.Interfaces.Services
{
    public interface ICommentService
    {
        CommentEntity GetCommentEntity(int id);

        IEnumerable<CommentEntity> GetCommentsByCreationDate
        (int articleId, int takeCount,
            int skipCount = 0, bool ascending = false);

        void CreateComment(CommentEntity comment);
        void DeleteComment(CommentEntity comment);
        void UpdateComment(CommentEntity comment);
    }
}
