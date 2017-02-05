using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces.DTO;

namespace DAL.Interfaces.Repository
{
    public interface ITagRepository
    {
        IEnumerable<DalTag> GetByPrefix(string prefix);
    }
}
