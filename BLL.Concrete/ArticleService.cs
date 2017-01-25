using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BLL.Concrete.Mappers;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Services;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Repository;
using ExpressionTreeVisitor;

namespace BLL.Concrete
{
    public class ArticleService : IArticleService
    {
        private IUnitOfWork unitOfWork;
        private IArticleRepository articleRepository;

        public ArticleService(IUnitOfWork unitOfWork, IArticleRepository articleRepository)
        {
            this.unitOfWork = unitOfWork;
            this.articleRepository = articleRepository;
        }

        public void CreateArticle(ArticleEntity article)
        {
            articleRepository.Create(article.ToDalArticle());
            unitOfWork.Commit();
        }

        public void DeleteArticle(ArticleEntity article)
        {
            articleRepository.Delete(article.ToDalArticle());
            unitOfWork.Commit();
        }

        public IEnumerable<ArticleEntity> GetArticleEntities
            (int takeCount, int skipCount = 0, 
            Expression<Func<ArticleEntity, int>> orderSelector = null)
        {
            var expressionModifier = new ExpressionModifier();
            var dalOrderSelector = orderSelector == null
                    ? null
                    :(Expression<Func<DalArticle, int>>) expressionModifier
                            .Modify<DalArticle>(orderSelector);
            return articleRepository.GetEntities(takeCount, skipCount, dalOrderSelector)
                    .Select(article => article.ToArticleEntity());
        }

        public ArticleEntity GetArticleEntity(int id)
            => articleRepository.GetById(id).ToArticleEntity();

        public IEnumerable<ArticleEntity> GetArticlesByCreationDate
            (int takeCount, int skipCount = 0, bool ascending = false)
            => articleRepository.GetArticlesByCreationDate(takeCount, skipCount, ascending)
                .Select(article => article.ToArticleEntity());

        public IEnumerable<ArticleEntity> GetArticlesByPredicate
            (Expression<Func<ArticleEntity, bool>> predicate, int takeCount, 
            int skipCount = 0, Expression<Func<ArticleEntity, int>> orderSelector = null)
        {
            var expressionModifier = new ExpressionModifier();
            var dalPredicate = (Expression<Func<DalArticle, bool>>) 
                expressionModifier.Modify<DalArticle>(predicate);
            var dalOrderSelector = orderSelector == null
                ? null
                : (Expression<Func<DalArticle, int>>)expressionModifier
                            .Modify<DalArticle>(orderSelector);
            return articleRepository.GetEntitiesByPredicate(dalPredicate,
                    takeCount, skipCount, dalOrderSelector)
                .Select(article => article.ToArticleEntity());
        }

        public ArticleEntity GetByPredicate(Expression<Func<ArticleEntity, bool>> predicate)
        {
            var expressionModifier = new ExpressionModifier();
            var dalPredicate = (Expression<Func<DalArticle, bool>>)
                expressionModifier.Modify<DalArticle>(predicate);
            return articleRepository.GetByPredicate(dalPredicate).ToArticleEntity();
        }

        public void UpdateArticle(ArticleEntity article)
        {
            articleRepository.Update(article.ToDalArticle());
            unitOfWork.Commit();
        }
    }
}
