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
    public class ArticleRepository : IArticleRepository
    {
        private DbContext context;

        public ArticleRepository(DbContext context)
        {
            this.context = context;
        }

        public void Create(DalArticle article)
        {
            context.Set<Article>().Add(article.ToOrmArticle());
        }

        public void Delete(DalArticle article)
        {
            var ormArticle = context.Set<Article>().Single(a => a.Id == article.Id);
            context.Set<Article>().Remove(ormArticle);
        }

        public IEnumerable<DalArticle> GetArticlesByCreationDate
            (int takeCount, int skipCount = 0, bool ascending = false)
        {
            if (ascending)
                return context.Set<Article>().OrderBy(article => article.DateAdded)
                    .Include(article => article.Tags)
                    .Skip(skipCount).Take(takeCount).ToArray()
                    .Select(article => article.ToDalArticle());
            return context.Set<Article>().OrderByDescending
                (article => article.DateAdded)
                .Include(article => article.Tags)
                .Skip(skipCount).Take(takeCount).ToArray()
                .Select(article => article.ToDalArticle());
        }

        public IEnumerable<DalArticle> GetArticlesByUser
            (int userId, int takeCount, int skipCount = 0, bool ascending = false)
        {
            if (ascending)
                return context.Set<Article>()
                    .Where(article => article.Blog.UserId == userId)
                    .OrderBy(article => article.DateAdded)
                    .Include(article => article.Tags)
                    .Skip(skipCount).Take(takeCount).ToArray()
                    .Select(article => article.ToDalArticle());
            return context.Set<Article>()
                    .Where(article => article.Blog.UserId == userId)
                    .OrderByDescending(article => article.DateAdded)
                    .Include(article => article.Tags)
                    .Skip(skipCount).Take(takeCount).ToArray()
                    .Select(article => article.ToDalArticle());
        }

        public DalArticle GetById(int id)
            => context.Set<Article>()
                .FirstOrDefault(article => article.Id == id)?.ToDalArticle();

        public DalArticle GetByPredicate(Expression<Func<DalArticle, bool>> f)
        {
            var expressionModifier = new ExpressionModifier();
            var ormExpression = expressionModifier.Modify<Article>(f);
            return context.Set<Article>().FirstOrDefault
                ((Expression<Func<Article, bool>>)ormExpression)?.ToDalArticle();
        }

        public IEnumerable<DalArticle> GetEntities
            (int takeCount, int skipCount = 0, 
            Expression<Func<DalArticle, int>> orderSelector = null)
        {
            if (orderSelector == null)
                orderSelector = article => article.Id;
            var expressionModifier = new ExpressionModifier();
            var ormOrderSelector = expressionModifier.Modify<Article>(orderSelector);
            return context.Set<Article>().OrderBy(
                (Expression<Func<Article, int>>)ormOrderSelector)
                .Include(article => article.Tags)
                .Skip(skipCount).Take(takeCount)
                .ToArray().Select(article => article.ToDalArticle());
        }

        public IEnumerable<DalArticle> GetEntitiesByPredicate
            (Expression<Func<DalArticle, bool>> f, int takeCount, int skipCount = 0, 
            Expression<Func<DalArticle, int>> orderSelector = null)
        {
            if (orderSelector == null)
                orderSelector = article => article.Id;
            var expressionModifier = new ExpressionModifier();
            var ormExpression = expressionModifier.Modify<Article>(f);
            var ormOrderSelector = expressionModifier.Modify<Article>(orderSelector);
            return context.Set<Article>().Where
                ((Expression<Func<Article, bool>>)ormExpression).OrderBy
                ((Expression<Func<Article, int>>)ormOrderSelector)
                .Include(article => article.Tags)
                .Skip(skipCount).Take(takeCount).ToArray()
                .Select(article => article.ToDalArticle());
        }

        public void Update(DalArticle dalArticle)
        {
            var ormArticle = context.Set<Article>()
                    .FirstOrDefault(article => article.Id == dalArticle.Id);
            if (ormArticle == null)
                throw new ArgumentException
                    ($"Blog with Id = {dalArticle.Id} does not exists");
            ormArticle.BlogId = dalArticle.BlogId;
            ormArticle.Content = dalArticle.Content;
            ormArticle.DateAdded = dalArticle.DateAdded;
            ormArticle.Name = dalArticle.Name;
            ormArticle.Rating = dalArticle.Rating;
            context.Entry(ormArticle).State = EntityState.Modified;
        }
    }
}
