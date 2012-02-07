﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Kevin.Infrastructure.Querying.Expressions
{
    using Extensions;

    public static class ExpressionBuilder
    {
        public static Expression<Func<TModel, TValue>> Expression<TModel, TValue>(string memberName)
        {
            return (Expression<Func<TModel, TValue>>)Lambda<TModel>(memberName);
        }

        public static LambdaExpression Lambda<T>(string memberName)
        {
            return Lambda<T>(memberName, false);
        }

        public static LambdaExpression Lambda<T>(Type memberType, string memberName, bool checkForNull)
        {
            MemberAccessExpressionBuilderBase expressionBuilder = ExpressionBuilderFactory.MemberAccess(typeof(T), memberType, memberName, checkForNull);

            return expressionBuilder.CreateLambdaExpression();
        }

        public static LambdaExpression Lambda<T>(string memberName, bool checkForNull)
        {
            MemberAccessExpressionBuilderBase expressionBuilder = ExpressionBuilderFactory.MemberAccess(typeof(T), memberName, checkForNull);

            return expressionBuilder.CreateLambdaExpression();
        }

        public static Expression<Func<T, bool>> Expression<T>(IEnumerable<IFilterDescriptor> filterDescriptors)
        {
            ParameterExpression parameterExpression = System.Linq.Expressions.Expression.Parameter(typeof(T), "item");
            return (Expression<Func<T, bool>>)new FilterDescriptorCollectionExpressionBuilder(parameterExpression, filterDescriptors).CreateFilterExpression();
        }

        //add
        public static Expression<Func<T, bool>> Expression<T>(IFilterDescriptor filterDescriptor)
        {
            return Expression<T>(new IFilterDescriptor[] { filterDescriptor });
        }
    }
}
