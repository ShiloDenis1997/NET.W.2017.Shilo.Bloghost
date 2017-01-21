using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            User ormUser = context.Set<User>().Single(u => u.id == user.Id);
            context.Set<User>().Remove(ormUser);
        }

        public IEnumerable<DalUser> GetAll()
        {
            return context.Set<User>().Select(user => user.ToDalUser());
        }

        public DalUser GetById(int key)
        {
            User ormUser = context.Set<User>().FirstOrDefault(user => user.id == key);
            return ormUser?.ToDalUser();
        }

        public DalUser GetByPredicate(Expression<Func<DalUser, bool>> f)
        {
            var expressionModifier = new DalUserExpressionModifier();
            var ormPredicate = expressionModifier.Modify(f);
            User user = context.Set<User>().First((Expression<Func<User,bool>>)ormPredicate);
            return user?.ToDalUser();
        }

        public void Update(DalUser entity)
        {
            throw new NotImplementedException();
        }

        private class DalUserExpressionModifier : ExpressionVisitor
        {
            public Expression Modify(Expression expression)
            {
                return Visit(expression);
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                if (node.Type == typeof(DalUser))
                    return Expression.Parameter(typeof(User));
                return base.VisitParameter(node);
            }
        }
    }
}
