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
            context.Set<User>().Add(ormUser);
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
                ((Expression<Func<User, bool>>) ormPredicate)?.ToDalUser();
        }

        public IEnumerable<DalUser> GetEntities(int takeCount, int skipCount = 0)
        {
            var expressionModifier = new ExpressionModifier();
            return context.Set<User>()
                .OrderByDescending(user => user.DateRegistered)
                .Skip(skipCount).Take(takeCount)
                .ToArray().Select(user => user.ToDalUser());
        }

        public IEnumerable<DalUser> GetEntitiesByPredicate
            (Expression<Func<DalUser, bool>> f, int takeCount, int skipCount = 0)
        {
            var expressionModifier = new ExpressionModifier();
            var ormPredicate = expressionModifier.Modify<User>(f);
            IEnumerable<User> users = context.Set<User>()
                .Where((Expression<Func<User, bool>>)ormPredicate)
                .OrderByDescending(user => user.DateRegistered)
                .Skip(skipCount).Take(takeCount);
            return users.Select(user => user.ToDalUser());
        }

        public void Update(DalUser dalUser)
        {
            User ormUser = context.Set<User>().FirstOrDefault(u => u.Id == dalUser.Id);
            if (ormUser == null)
                throw new ArgumentException($"User with id {dalUser.Id} does not exist");
            ormUser.Roles = dalUser.Roles.Select(roleName => context.Set<Role>()
                .FirstOrDefault(role => role.Rolename.Equals(roleName))).ToArray();
            ormUser.DateRegistered = dalUser.DateRegistered;
            ormUser.Login = dalUser.Login;
            ormUser.Email = dalUser.Email;
            ormUser.Firstname = dalUser.Firstname;
            ormUser.Password = dalUser.Password;
            ormUser.Secondname = dalUser.Secondname;
            ormUser.Thirdname = dalUser.Thirdname;
            context.Entry(ormUser).State = EntityState.Modified;
        }
    }
}
