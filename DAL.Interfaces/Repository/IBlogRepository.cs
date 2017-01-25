using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces.DTO;

namespace DAL.Interfaces.Repository
{
    public interface IBlogRepository : IRepository<DalBlog>
    {
        IEnumerable<DalBlog> GetBlogsByCreationDate
            (int takeCount, int skipCount = 0, bool ascending = false);
    }
}
