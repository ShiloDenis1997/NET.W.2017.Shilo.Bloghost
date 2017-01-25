using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.Entities;

namespace BLL.Interfaces.Services
{
    public interface IArticleService
    {
        ArticleEntity GetArticleEntity(int id);

        IEnumerable<ArticleEntity> GetArticleEntities(int takeCount, int skipCount = 0,
            Expression<Func<ArticleEntity, int>> orderSelector = null);

        IEnumerable<ArticleEntity> GetArticlesByPredicate
        (Expression<Func<ArticleEntity, bool>> predicate,
            int takeCount, int skipCount = 0,
            Expression<Func<ArticleEntity, int>> orderSelector = null);

        IEnumerable<ArticleEntity> GetArticlesByCreationDate
            (int takeCount, int skipCount = 0, bool ascending = false);

        ArticleEntity GetByPredicate(Expression<Func<ArticleEntity, bool>> predicate);

        void CreateArticle(ArticleEntity article);
        void DeleteArticle(ArticleEntity article);
        void UpdateArticle(ArticleEntity article);
    }
}
