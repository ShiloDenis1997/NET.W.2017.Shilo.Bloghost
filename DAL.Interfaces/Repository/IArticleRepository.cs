using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces.DTO;

namespace DAL.Interfaces.Repository
{
    public interface IArticleRepository : IRepository<DalArticle>
    {
        IEnumerable<DalArticle> GetArticlesByCreationDate
            (int takeCount, int skipCount = 0, bool ascending = false);
    }
}
