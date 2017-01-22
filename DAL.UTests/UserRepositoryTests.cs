using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;
using DAL.Concrete;
using DAL.Interfaces.DTO;
using Moq;
using NUnit;
using NUnit.Framework;
using ORM;

namespace DAL.UTests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        [Test]
        public void ExpressionModifierExecutionTest()
        {
            //arrange
            var modifier = new PredicateVisitor();
            Expression<Func<DalUser, bool>> dalExpression = user => user.Id == 5;
            Expression<Func<User, bool>> ormExpression =
                    (Expression<Func<User, bool>>)modifier.ModifyPredicate<User>(dalExpression);
            User[] ormUsers =
            {
                new User {Id=3},
                new User {Id=5},
                new User {Id = 8},
            };
            //act
            var actualUsers = ormUsers.Where(ormExpression.Compile());
            //assert
            Assert.AreEqual(1, actualUsers.Count());
        }
    }
}
