using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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
            var ormArticle = article.ToOrmArticle();
            IEnumerable<Tag> tags =
                article.Tags.Where(tag => !string.IsNullOrWhiteSpace(tag))
                .Select(tag => context.Set<Tag>().FirstOrDefault(t => t.Name.Equals(tag)) 
                                ?? context.Set<Tag>().Add(new Tag {Name = tag}));
            ormArticle.Tags = tags.ToArray();
            context.Set<Article>().Add(ormArticle);
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

        public IEnumerable<DalArticle> GetArticlesByPopularity
            (int takeCount, int skipCount = 0, bool ascending = false)
        {
            if (ascending)
                return context.Set<Article>()
                    .OrderBy(article => article.Rating)
                    .Skip(skipCount).Take(takeCount).ToArray()
                    .Select(article => article.ToDalArticle());
            else
                return context.Set<Article>()
                    .OrderByDescending(article => article.Rating)
                    .Skip(skipCount).Take(takeCount).ToArray()
                    .Select(article => article.ToDalArticle());
        }

        public IEnumerable<DalArticle> GetArticlesByTag
            (string tag, int takeCount, int skipCount = 0, bool ascending = false)
        {
            var ormTag = context.Set<Tag>().FirstOrDefault
                (t => string.Compare(tag, t.Name, StringComparison.OrdinalIgnoreCase) == 0);
            if (ormTag == null)
                return null;
            if (ascending)
                return context.Set<Article>()
                    .Where(article => article.Tags.Any(t => t.Id == ormTag.Id))
                    .OrderBy(article => article.DateAdded)
                    .Skip(skipCount).Take(takeCount).ToArray()
                    .Select(article => article.ToDalArticle());
            else
            {
                return context.Set<Article>()
                    .Where(article => article.Tags.Any(t => t.Id == ormTag.Id))
                    .OrderByDescending(article => article.DateAdded)
                    .Skip(skipCount).Take(takeCount).ToArray()
                    .Select(article => article.ToDalArticle());
            }
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

        public IEnumerable<DalArticle> GetArticlesWithText
            (string text, int takeCount, int skipCount = 0, bool ascending = false)
        {
            if (ascending)
                return context.Set<Article>()
                    .Where(article => article.Content.Contains(text))
                    .OrderBy(article => article.DateAdded)
                    .Skip(skipCount).Take(takeCount).ToList()
                    .Select(article => article.ToDalArticle());
            return context.Set<Article>()
                    .Where(article => article.Content.Contains(text))
                    .OrderByDescending(article => article.DateAdded)
                    .Skip(skipCount).Take(takeCount).ToList()
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
            (int takeCount, int skipCount = 0)
        {
            var expressionModifier = new ExpressionModifier();
            return context.Set<Article>()
                .OrderByDescending(article => article.DateAdded)
                .Include(article => article.Tags)
                .Skip(skipCount).Take(takeCount)
                .ToArray().Select(article => article.ToDalArticle());
        }

        public IEnumerable<DalArticle> GetEntitiesByPredicate
            (Expression<Func<DalArticle, bool>> f, int takeCount, int skipCount = 0)
        {
            var expressionModifier = new ExpressionModifier();
            var ormExpression = expressionModifier.Modify<Article>(f);
            return context.Set<Article>().Where
                ((Expression<Func<Article, bool>>)ormExpression)
                .OrderByDescending(article => article.DateAdded)
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
            IEnumerable<Tag> tags =
                dalArticle.Tags.Where(tag => !string.IsNullOrWhiteSpace(tag))
                .Select(tag => context.Set<Tag>().FirstOrDefault(t => t.Name.Equals(tag))
                                ?? context.Set<Tag>().Add(new Tag { Name = tag }));
            ormArticle.Tags.Clear();
            foreach (var tag in tags)
            {
                if (ormArticle.Tags.All(t => t.Id != tag.Id))
                    ormArticle.Tags.Add(tag);
            }
            context.Entry(ormArticle).State = EntityState.Modified;
        }
    }
}
