using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces.DTO;

namespace DAL.Interfaces.Repository
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        IEnumerable<TEntity> GetEntities(int takeCount, int skipCount = 0);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetEntitiesByPredicate
            (Expression<Func<TEntity, bool>> f, int takeCount, int skipCount = 0);
        TEntity GetByPredicate(Expression<Func<TEntity, bool>> f);
        void Create(TEntity e);
        void Delete(TEntity e);
        void Update(TEntity e);
    }
}
