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
            context.Set<User>().Add(user.ToOrmUser());
        }

        public void Delete(DalUser user)
        {
            User ormUser = context.Set<User>().Single(u => u.Id == user.Id);
            context.Set<User>().Remove(ormUser);
        }

        public IEnumerable<DalUser> GetAll()
        {
            return context.Set<User>().Select(user => user.ToDalUser());
        }

        public DalUser GetById(int key)
        {
            User ormUser = context.Set<User>().FirstOrDefault(user => user.Id == key);
            return ormUser?.ToDalUser();
        }

        public DalUser GetByPredicate(Expression<Func<DalUser, bool>> f)
        {
            var expressionModifier = new PredicateVisitor();
            var ormPredicate = expressionModifier.ModifyPredicate<User>(f);
            User user = context.Set<User>().First((Expression<Func<User,bool>>)ormPredicate);
            return user?.ToDalUser();
        }

        public void Update(DalUser dalUser)
        {
            var ormUser = dalUser.ToOrmUser();
            User user = context.Set<User>().First(u => u.Id == ormUser.Id);
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
