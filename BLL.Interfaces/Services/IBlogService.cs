using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.Entities;

namespace BLL.Interfaces.Services
{
    public interface IBlogService
    {
        BlogEntity GetBlogEntity(int id);

        IEnumerable<BlogEntity> GetBlogEntities(int takeCount, int skipCount = 0,
            Expression<Func<BlogEntity, int>> orderSelector = null);

        IEnumerable<BlogEntity> GetBlogsByPredicate
        (Expression<Func<BlogEntity, bool>> predicate,
            int takeCount, int skipCount = 0, 
            Expression<Func<BlogEntity, int>> orderSelector = null);

        IEnumerable<BlogEntity> GetBlogsByCreationDate
            (int takeCount, int skipCount = 0, bool ascending = false);

        BlogEntity GetByPredicate(Expression<Func<BlogEntity, bool>> predicate);

        void CreateBlog(BlogEntity blog);
        void DeleteBlog(BlogEntity blog);
        void UpdateBlog(BlogEntity blog);
    }
}
