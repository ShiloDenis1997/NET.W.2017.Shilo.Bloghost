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
    public class UserService : IUserService
    {
        private IUnitOfWork unitOfWork;
        private IRepository<DalUser> repository;

        public UserService(IUnitOfWork uow, IRepository<DalUser> repo)
        {
            unitOfWork = uow;
            repository = repo;
        }

        public void CreateUser(UserEntity user)
        {
            repository.Create(user.ToDalUser());
            unitOfWork.Commit();
        }

        public void DeleteUser(UserEntity user)
        {
            repository.Delete(user.ToDalUser());
            unitOfWork.Commit();
        }

        public IEnumerable<UserEntity> GetUserEntities
        (int takeCount, int skipCount = 0,
            Expression<Func<UserEntity, int>> orderSelector = null)
        {
            var expressionModifier = new ExpressionModifier();
            var dalSelector = orderSelector == null
                ? null
                :(Expression<Func<DalUser, int>>)
                expressionModifier.Modify<DalUser>(orderSelector);
            return repository.GetEntities(takeCount, skipCount, dalSelector)
                .Select(user => user.ToUserEntity());
        }

        public UserEntity GetUserEntity(int id)
            => repository.GetById(id)?.ToUserEntity();

        public void UpdateUser(UserEntity user)
        {
            try
            {
                repository.Update(user.ToDalUser());
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Cannot update user", ex);
            }
            
            unitOfWork.Commit();
        }
        
        public IEnumerable<UserEntity> GetUsersByPredicate
            (Expression<Func<UserEntity, bool>> predicate, 
                    int takeCount, int skipCount = 0,
                    Expression<Func<UserEntity, int>> orderSelector = null)
        {
            var expressionModifier = new ExpressionModifier();
            var dalPredicate = (Expression<Func<DalUser, bool>>)
                expressionModifier.Modify<DalUser>(predicate);
            var dalOrderSelector = (Expression<Func<DalUser, int>>)
                expressionModifier.Modify<DalUser>(predicate);
            return repository.GetEntitiesByPredicate(dalPredicate, takeCount, skipCount)
                .Select(user => user.ToUserEntity());
        }

        public UserEntity GetUserByPredicate
            (Expression<Func<UserEntity, bool>> predicate)
        {
            var predicateModifier = new ExpressionModifier();
            var dalPredicate = (Expression<Func<DalUser, bool>>)
                    predicateModifier.Modify<DalUser>(predicate);
            return repository.GetByPredicate(dalPredicate)?.ToUserEntity();
        }
    }
}
