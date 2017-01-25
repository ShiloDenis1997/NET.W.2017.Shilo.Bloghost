using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.Entities;

namespace BLL.Interfaces.Services
{
    public interface IUserService
    {
        UserEntity GetUserEntity(int id);
        UserEntity GetUserByPredicate(Expression<Func<UserEntity, bool>> f);

        IEnumerable<UserEntity> GetUserEntities(int takeCount, int skipCount = 0,
            Expression<Func<UserEntity, int>> orderSelector = null);

        IEnumerable<UserEntity> GetUsersByPredicate
        (Expression<Func<UserEntity, bool>> predicate,
            int takeCount, int skipCount = 0,
            Expression<Func<UserEntity, int>> orderSelector = null);

        void CreateUser(UserEntity user);
        void DeleteUser(UserEntity user);
        void UpdateUser(UserEntity user);
    }
}
