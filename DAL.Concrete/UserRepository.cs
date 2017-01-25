using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DAL.Concrete.Mappers;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Repository;
using ExpressionTreeVisitor;
using ORM;

namespace DAL.Concrete
{
    public class UserRepository : IRepository<DalUser>
    {
        private DbContext context;

        public UserRepository(DbContext context)
        {
            this.context = context;
        }

        public void Create(DalUser user)
        {
            var ormUser = user.ToOrmUser();
            ormUser.Roles = user.Roles.Select(rolename => context.Set<Role>()
                .First(role => role.Rolename == rolename)).ToArray();
            context.Set<User>().Add(user.ToOrmUser());
        }

        public void Delete(DalUser user)
        {
            User ormUser = context.Set<User>().Single(u => u.Id == user.Id);
            context.Set<User>().Remove(ormUser);
        }

        public DalUser GetById(int id)
        {
            User ormUser = context.Set<User>().FirstOrDefault(user => user.Id == id);
            return ormUser?.ToDalUser();
        }

        public DalUser GetByPredicate(Expression<Func<DalUser, bool>> f)
        {
            var expressionModifier = new ExpressionModifier();
            var ormPredicate = expressionModifier.Modify<User>(f);
            return context.Set<User>().FirstOrDefault
                ((Expression<Func<User, bool>>) ormPredicate).ToDalUser();
        }

        public IEnumerable<DalUser> GetEntities(int takeCount, int skipCount = 0,
            Expression<Func<DalUser, int>> orderSelector = null)
        {
            if (orderSelector == null)
                orderSelector = user => user.Id;
            var expressionModifier = new ExpressionModifier();
            var ormOrderSelector = expressionModifier.Modify<User>(orderSelector);
            return context.Set<User>().OrderBy(
                (Expression<Func<User, int>>)ormOrderSelector)
                .Skip(skipCount).Take(takeCount)
                .ToArray().Select(user => user.ToDalUser());
        }

        public IEnumerable<DalUser> GetEntitiesByPredicate
            (Expression<Func<DalUser, bool>> f, int takeCount, int skipCount = 0,
            Expression<Func<DalUser, int>> orderSelector = null )
        {
            if (orderSelector == null)
                orderSelector = user => user.Id;
            var expressionModifier = new ExpressionModifier();
            var ormPredicate = expressionModifier.Modify<User>(f);
            var ormOrderSelector = expressionModifier.Modify<User>(orderSelector);
            IEnumerable<User> users = context.Set<User>().Where
                ((Expression<Func<User, bool>>)ormPredicate).OrderBy
                ((Expression<Func<User, int>>)ormOrderSelector)
                .Skip(skipCount).Take(takeCount);
            return users.Select(user => user.ToDalUser());
        }

        public void Update(DalUser dalUser)
        {
            var ormUser = dalUser.ToOrmUser();
            User user = context.Set<User>().FirstOrDefault(u => u.Id == ormUser.Id);
            if (user == null)
                throw new ArgumentException($"User with id {dalUser.Id} does not exist");
            user.Roles = dalUser.Roles.Select(roleName => context.Set<Role>()
                .FirstOrDefault(role => role.Rolename.Equals(roleName))).ToArray();
            user.DateRegistered = dalUser.DateRegistered;
            user.Login = dalUser.Login;
            user.Email = dalUser.Email;
            user.Firstname = dalUser.Firstname;
            user.Password = dalUser.Password;
            user.Secondname = dalUser.Secondname;
            user.Thirdname = dalUser.Thirdname;
        }
    }
}
