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
        private IRepository<DalBlog> blogRepository;

        public BlogService(IUnitOfWork uow, IRepository<DalBlog> repo)
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

        public IEnumerable<BlogEntity> GetAllBlogEntities()
            => blogRepository.GetAll().Select(blog => blog.ToBlogEntity());

        public BlogEntity GetBlogEntity(int id)
            => blogRepository.GetById(id).ToBlogEntity();

        public BlogEntity GetByPredicate
            (Expression<Func<BlogEntity, bool>> predicate)
        {
            var expressionModifier = new PredicateVisitor();
            var dalPredicate = expressionModifier
                .ModifyPredicate<DalBlog>(predicate);
            return blogRepository.GetByPredicate
                ((Expression<Func<DalBlog, bool>>) dalPredicate)
                    .ToBlogEntity();
        }

        public void UpdateBlog(BlogEntity blog)
        {
            blogRepository.Update(blog.ToDalBlog());
            unitOfWork.Commit();
        }
    }
}
