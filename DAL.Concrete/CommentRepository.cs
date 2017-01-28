using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Concrete.Mappers;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Repository;
using ExpressionTreeVisitor;
using ORM;

namespace DAL.Concrete
{
    public class CommentRepository : ICommentRepository
    {
        private DbContext context;

        public CommentRepository(DbContext context)
        {
            this.context = context;
        }

        public void Create(DalComment comment)
        {
            context.Set<Comment>().Add(comment.ToOrmComment());
        }

        public void Delete(DalComment comment)
        {
            var ormComment = context.Set<Comment>()
                .Single(c => c.Id == comment.Id);
            context.Set<Comment>().Remove(ormComment);
        }

        public DalComment GetById(int id)
        {
            return context.Set<Comment>()
                .FirstOrDefault(comment => comment.Id == id)
                ?.ToDalComment();

        }

        public DalComment GetByPredicate
            (Expression<Func<DalComment, bool>> predicate)
        {
            var expressionModifier = new ExpressionModifier();
            var ormPredicate = (Expression<Func<Comment, bool>>) 
                expressionModifier.Modify<Comment>(predicate);
            var ormComment = context.Set<Comment>().FirstOrDefault(ormPredicate);
            return ormComment?.ToDalComment();
        }

        public IEnumerable<DalComment> GetCommentsByCreationDate
            (int articleId, int takeCount, 
            int skipCount = 0, bool ascending = false)
        {
            if (ascending)
                return context.Set<Comment>()
                    .Where(comment => comment.ArticleId == articleId)
                    .OrderBy(comment => comment.DateAdded)
                    .Skip(skipCount).Take(takeCount).ToList()
                    .Select(comment => comment.ToDalComment());
            else
                return context.Set<Comment>()
                    .Where(comment => comment.ArticleId == articleId)
                    .OrderByDescending(comment => comment.DateAdded)
                    .Skip(skipCount).Take(takeCount).ToList()
                    .Select(comment => comment.ToDalComment());
        }

        public IEnumerable<DalComment> GetEntities
            (int takeCount, int skipCount = 0, 
            Expression<Func<DalComment, int>> orderSelector = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DalComment> GetEntitiesByPredicate
            (Expression<Func<DalComment, bool>> predicate, 
            int takeCount, int skipCount = 0, 
            Expression<Func<DalComment, int>> orderSelector = null)
        {
            if (orderSelector == null)
                orderSelector = comment => comment.Id;
            var expressionModifier = new ExpressionModifier();
            var ormExpression = expressionModifier.Modify<Comment>(predicate);
            var ormOrderSelector = expressionModifier.Modify<Comment>(orderSelector);
            return context.Set<Comment>().Where
                ((Expression<Func<Comment, bool>>)ormExpression).OrderBy
                ((Expression<Func<Comment, int>>)ormOrderSelector)
                .Skip(skipCount).Take(takeCount).ToArray().Select(c => c.ToDalComment());
        }

        public DalComment GetLastUserComment(int articleId, int userId)
        {
            return context.Set<Comment>()
                .Where(comment => (comment.ArticleId == articleId) && (comment.UserId == userId))
                .OrderByDescending(comment => comment.DateAdded)
                .FirstOrDefault()?.ToDalComment();
        }

        public void Update(DalComment dalComment)
        {
            var ormComment = context.Set<Comment>()
                    .FirstOrDefault(c => c.Id == dalComment.Id);
            if (ormComment == null)
                throw new ArgumentException
                    ($"Comment with Id = {dalComment.Id} does not exists");
            ormComment.Content = dalComment.Content;
            ormComment.Rating = dalComment.Rating;
            ormComment.DateAdded = dalComment.DateAdded;
            context.Entry(ormComment).State = EntityState.Modified;
        }
    }
}
