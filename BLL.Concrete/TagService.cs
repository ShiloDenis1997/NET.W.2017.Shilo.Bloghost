using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Concrete.Mappers;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Services;
using DAL.Interfaces.Repository;

namespace BLL.Concrete
{
    public class TagService : ITagService
    {
        private ITagRepository tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        public IEnumerable<TagEntity> GetTagsByPrefix(string prefix)
        {
            return tagRepository.GetByPrefix(prefix)
                ?.Select(tag => tag.ToBllTag());
        }
    }
}
