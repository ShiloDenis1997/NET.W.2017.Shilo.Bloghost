using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Concrete.Mappers;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Services;
using DAL.Interfaces.Repository;

namespace BLL.Concrete
{
    public class CommentService : ICommentService
    {
        private ICommentRepository commentRepository;
        private IUnitOfWork unitOfWork;

        public CommentService(ICommentRepository commentRepository,
            IUnitOfWork unitOfWork)
        {
            this.commentRepository = commentRepository;
            this.unitOfWork = unitOfWork;
        }

        public void CreateComment(CommentEntity comment)
        {
            commentRepository.Create(comment.ToDalComment());
            unitOfWork.Commit();
        }

        public void DeleteComment(CommentEntity comment)
        {
            commentRepository.Delete(comment.ToDalComment());
            unitOfWork.Commit();
        }

        public CommentEntity GetCommentEntity(int id)
            => commentRepository.GetById(id)?.ToBllComment();

        public IEnumerable<CommentEntity> GetCommentsByCreationDate
            (int articleId, int takeCount, int skipCount = 0, 
                bool ascending = false)
        {
            return commentRepository.GetCommentsByCreationDate(
                    articleId, takeCount, skipCount, ascending)
                .Select(comment => comment.ToBllComment());
        }

        public void UpdateComment(CommentEntity comment)
        {
            commentRepository.Update(comment.ToDalComment());
            unitOfWork.Commit();
        }
    }
}
