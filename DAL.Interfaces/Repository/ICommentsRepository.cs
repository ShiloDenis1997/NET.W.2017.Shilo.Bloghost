using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces.DTO;

namespace DAL.Interfaces.Repository
{
    public interface ICommentsRepository : IRepository<DalComment>
    {
        IEnumerable<DalComment> GetCommentsByCreationDate
            (int takeCount, int skipCount = 0, bool ascending = false);
    }
}
