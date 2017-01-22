﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace DAL.Concrete
{
    public class PredicateVisitor : ExpressionVisitor
    {
        private ParameterExpression parameter;

        public Expression ModifyPredicate<RType>(Expression expression)
        {
            parameter = Expression.Parameter(typeof(RType));
            return Visit(expression);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return parameter;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            Expression exp = Visit(node.Expression);
            if (exp != node.Expression)
            {
                return Expression.PropertyOrField(exp, node.Member.Name);
            }
            return node;
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            Expression exp = Visit(node.Body);
            return Expression.Lambda(exp,
                node.Parameters.Select
                    (par => (ParameterExpression)VisitParameter(par)));
        }
    }
}