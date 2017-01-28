using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces.DTO;

namespace DAL.Interfaces.Repository
{
    public interface ICommentRepository : IRepository<DalComment>
    {
        IEnumerable<DalComment> GetCommentsByCreationDate
            (int articleId, int takeCount, 
            int skipCount = 0, bool ascending = false);

        DalComment GetLastUserComment(int articleId, int userId);
    }
}
