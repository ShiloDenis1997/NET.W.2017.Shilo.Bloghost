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
    public class BlogService : IBlogService
    {
        private IUnitOfWork unitOfWork;
        private IBlogRepository blogRepository;

        public BlogService(IUnitOfWork uow, IBlogRepository repo)
        {
            unitOfWork = uow;
            blogRepository = repo;
        }

        public void CreateBlog(BlogEntity blog)
        {
            blogRepository.Create(blog.ToDalBlog());
            unitOfWork.Commit();
        }

        public void DeleteBlog(BlogEntity blog)
        {
            blogRepository.Delete(blog.ToDalBlog());
            unitOfWork.Commit();
        }
        

        public BlogEntity GetBlogEntity(int id)
            => blogRepository.GetById(id).ToBlogEntity();

        public void UpdateBlog(BlogEntity blog)
        {
            blogRepository.Update(blog.ToDalBlog());
            unitOfWork.Commit();
        }

        public IEnumerable<BlogEntity> GetBlogEntities
            (int takeCount, int skipCount = 0, 
            Expression<Func<BlogEntity, int>> orderSelector = null)
        {
            var expressionModifier = new ExpressionModifier();
            var ormOrderSelector = orderSelector == null
                ? null
                : expressionModifier.Modify<DalBlog>(orderSelector);
            return blogRepository.GetEntities(takeCount, skipCount,
                (Expression<Func<DalBlog, int>>)ormOrderSelector)
                .Select(blog => blog.ToBlogEntity());
        }

        public IEnumerable<BlogEntity> GetBlogsByPredicate
            (Expression<Func<BlogEntity, bool>> predicate, int takeCount,
            int skipCount = 0, Expression<Func<BlogEntity, int>> orderSelector = null)
        {
            var expressionModifier = new ExpressionModifier();
            var dalPredicate = expressionModifier
                .Modify<DalBlog>(predicate);
            var ormOrderSelector = orderSelector == null 
                ? null 
                : expressionModifier.Modify<DalBlog>(orderSelector);
            return blogRepository.GetEntitiesByPredicate
                ((Expression<Func<DalBlog, bool>>)dalPredicate,
                takeCount, skipCount, (Expression<Func<DalBlog, int>>)ormOrderSelector)
                .Select(blog => blog.ToBlogEntity());
        }

        public IEnumerable<BlogEntity> GetBlogsByCreationDate
            (int takeCount, int skipCount = 0, bool ascending = true)
        {
            return blogRepository.GetBlogsByCreationDate(takeCount, skipCount, ascending)
                .Select(blog => blog.ToBlogEntity());
        }

        public BlogEntity GetByPredicate(Expression<Func<BlogEntity, bool>> predicate)
        {
            var expressionModifier = new ExpressionModifier();
            var dalPredicate = expressionModifier
                .Modify<DalBlog>(predicate);
            return blogRepository.GetByPredicate(
                (Expression<Func<DalBlog, bool>>)dalPredicate).ToBlogEntity();
        }
    }
}
