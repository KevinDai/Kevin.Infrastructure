﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace Kevin.Infrastructure.Querying.Expressions
{
    public class SortDescriptorCollectionExpressionBuilder
    {
        private readonly IEnumerable<SortDescriptor> sortDescriptors;
        private readonly IQueryable queryable;

        public SortDescriptorCollectionExpressionBuilder(IQueryable queryable, IEnumerable<SortDescriptor> sortDescriptors)
        {
            this.queryable = queryable;
            this.sortDescriptors = sortDescriptors;
        }

        public IQueryable Sort()
        {
            IQueryable query = queryable;
            bool isFirst = true;

            foreach (var descriptor in this.sortDescriptors)
            {
                Type memberType = typeof(object);
                var descriptorBuilder = ExpressionBuilderFactory.MemberAccess(queryable, memberType, descriptor.Member);
                var expression = descriptorBuilder.CreateLambdaExpression();

                string methodName = "";
                if (isFirst)
                {
                    methodName = descriptor.SortDirection == ListSortDirection.Ascending ?
                        "OrderBy" : "OrderByDescending";
                    isFirst = false;
                }
                else
                {
                    methodName = descriptor.SortDirection == ListSortDirection.Ascending ?
                        "ThenBy" : "ThenByDescending";
                }

                query = query.Provider.CreateQuery(
                    Expression.Call(
                        typeof(Queryable),
                        methodName,
                        new[] { query.ElementType, expression.Body.Type },
                        query.Expression,
                        Expression.Quote(expression)));

            }
            return query;
        }
    }
}
