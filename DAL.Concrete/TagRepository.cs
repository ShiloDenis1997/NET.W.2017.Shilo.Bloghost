using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Concrete.Mappers;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Repository;
using ORM;

namespace DAL.Concrete
{
    public class TagRepository : ITagRepository
    {
        private DbContext context;

        public TagRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<DalTag> GetByPrefix(string prefix)
            => context.Set<Tag>()
                .Where(tag => tag.Name.StartsWith
                    (prefix))
                .ToArray().Select(tag => tag.ToDalTag());
    }
}
