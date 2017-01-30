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
        private IRepository<DalUser> userRepository;

        public UserService(IUnitOfWork uow, IRepository<DalUser> repo)
        {
            unitOfWork = uow;
            userRepository = repo;
        }

        public void CreateUser(UserEntity user)
        {
            userRepository.Create(user.ToDalUser());
            unitOfWork.Commit();
        }

        public void DeleteUser(UserEntity user)
        {
            userRepository.Delete(user.ToDalUser());
            unitOfWork.Commit();
        }

        public IEnumerable<UserEntity> GetUserEntities
        (int takeCount, int skipCount = 0)
        {
            return userRepository.GetEntities(takeCount, skipCount)
                ?.Select(user => user.ToBllUser());
        }

        public UserEntity GetUserEntity(int id)
            => userRepository.GetById(id)?.ToBllUser();

        public void UpdateUser(UserEntity user)
        {
            try
            {
                userRepository.Update(user.ToDalUser());
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Cannot update user", ex);
            }
            
            unitOfWork.Commit();
        }
        
        public IEnumerable<UserEntity> GetUsersByPredicate
            (Expression<Func<UserEntity, bool>> predicate, 
                    int takeCount, int skipCount = 0)
        {
            var expressionModifier = new ExpressionModifier();
            var dalPredicate = (Expression<Func<DalUser, bool>>)
                expressionModifier.Modify<DalUser>(predicate);
            return userRepository.GetEntitiesByPredicate(dalPredicate, takeCount, skipCount)
                ?.Select(user => user.ToBllUser());
        }

        public UserEntity GetUserByPredicate
            (Expression<Func<UserEntity, bool>> predicate)
        {
            var predicateModifier = new ExpressionModifier();
            var dalPredicate = (Expression<Func<DalUser, bool>>)
                    predicateModifier.Modify<DalUser>(predicate);
            return userRepository.GetByPredicate(dalPredicate)?.ToBllUser();
        }
    }
}
