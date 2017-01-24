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
    public class BlogRepository : IRepository<DalBlog>
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

        public IEnumerable<DalBlog> GetAll()
        {
            return context.Set<Blog>().Select(blog => blog.ToDalBlog());
        }

        public DalBlog GetById(int id)
        {
            return context.Set<Blog>()
                .FirstOrDefault(blog => blog.Id == id).ToDalBlog();
        }

        public DalBlog GetByPredicate(Expression<Func<DalBlog, bool>> f)
        {
            var expressionModifier = new PredicateVisitor();
            var ormExpression = expressionModifier.ModifyPredicate<Blog>(f);
            return context.Set<Blog>().FirstOrDefault
                ((Expression<Func<Blog, bool>>)ormExpression).ToDalBlog();
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
        }
    }
}
