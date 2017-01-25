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
    public class BlogRepository : IBlogRepository
    {
        private DbContext context;

        public BlogRepository(DbContext context)
        {
            this.context = context;
        }

        public void Create(DalBlog blog)
        {
            context.Set<Blog>().Add(blog.ToOrmBlog());
        }

        public void Delete(DalBlog blog)
        {
            var ormBlog = context.Set<Blog>().Single(b => b.Id == blog.Id);
            context.Set<Blog>().Remove(ormBlog);
        }

        public IEnumerable<DalBlog> GetBlogsByCreationDate
            (int takeCount, int skipCount = 0, bool ascending = false)
        {
            if (ascending)
                return context.Set<Blog>().OrderBy(blog => blog.DateStarted)
                    .Skip(skipCount).Take(takeCount).ToArray()
                    .Select(blog => blog.ToDalBlog());
            return context.Set<Blog>().OrderByDescending(blog => blog.DateStarted)
                .Skip(skipCount).Take(takeCount).ToArray()
                .Select(blog => blog.ToDalBlog());
        }

        public DalBlog GetById(int id)
        {
            return context.Set<Blog>()
                .FirstOrDefault(blog => blog.Id == id).ToDalBlog();
        }

        public DalBlog GetByPredicate(Expression<Func<DalBlog, bool>> f)
        {
            var expressionModifier = new ExpressionModifier();
            var ormExpression = expressionModifier.Modify<Blog>(f);
            return context.Set<Blog>().FirstOrDefault
                ((Expression<Func<Blog, bool>>)ormExpression)?.ToDalBlog();
        }

        public IEnumerable<DalBlog> GetEntities(int takeCount, int skipCount = 0,
            Expression<Func<DalBlog, int>> orderSelector = null)
        {
            if (orderSelector == null)
                orderSelector = blog => blog.Id;
            var expressionModifier = new ExpressionModifier();
            var ormOrderSelector = expressionModifier.Modify<Blog>(orderSelector);
            return context.Set<Blog>().OrderBy(
                (Expression<Func<Blog, int>>)ormOrderSelector)
                .Skip(skipCount).Take(takeCount)
                .ToArray().Select(blog => blog.ToDalBlog());
        }

        public IEnumerable<DalBlog> GetEntitiesByPredicate
            (Expression<Func<DalBlog, bool>> f, int takeCount, int skipCount = 0,
            Expression<Func<DalBlog, int>> orderSelector = null)
        {
            if (orderSelector == null)
                orderSelector = blog => blog.Id;
            var expressionModifier = new ExpressionModifier();
            var ormExpression = expressionModifier.Modify<Blog>(f);
            var ormOrderSelector = expressionModifier.Modify<Blog>(orderSelector);
            return context.Set<Blog>().Where
                ((Expression<Func<Blog, bool>>)ormExpression).OrderBy
                ((Expression<Func<Blog, int>>)ormOrderSelector)
                .Skip(skipCount).Take(takeCount).ToArray().Select(b => b.ToDalBlog());
        }

        public void Update(DalBlog dalBlog)
        {
            var ormBlog = context.Set<Blog>()
                    .FirstOrDefault(b => b.Id == dalBlog.Id);
            if (ormBlog == null)
                throw new ArgumentException
                    ($"Blog with Id = {dalBlog.Id} does not exists");
            ormBlog.DateStarted = dalBlog.DateStarted;
            ormBlog.Name = dalBlog.Name;
            ormBlog.Rating = dalBlog.Rating;
            ormBlog.UserId = dalBlog.UserId;
        }
    }
}
