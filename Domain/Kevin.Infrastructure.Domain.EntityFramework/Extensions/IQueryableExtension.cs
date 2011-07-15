﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Kevin.Infrastructure.Domain.EntityFramework.Extensions
{
    using Kevin.Infrastructure.Domain.Specification;

    public static class IQueryableExtension
    {
        internal static IQueryable<T> FindBy<T, TId>(this IQueryable<T> query, ISpecification<T> specification) where T : EntityBase<TId>
        {
            if (specification != null)
            {
                query = query.Where(specification.SatisfiedBy());
            }
            return query;
        }

        internal static IQueryable<T> Sort<T, TId>(this IQueryable<T> query, params SortDescriptor<T>[] sortDescriptors) where T : EntityBase<TId>
        {
            if (sortDescriptors != null && sortDescriptors.Any())
            {
                //反转排序说明对象列表
                var list = sortDescriptors.Reverse();
                foreach (var item in list)
                {
                    query = item.SortDirection == ListSortDirection.Ascending
                        ?
                        query.OrderBy(item.SortKeySelector)
                        :
                        query.OrderByDescending(item.SortKeySelector);
                }
            }
            else
            {
                query = query.OrderBy(e => e.Id);
            }
            return query;
        }

        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageIndex, int pageCount)
        {
            if (pageIndex <= 0)
            {
                throw new ArgumentException(
                    Resources.Messages.exception_PageIndexShouldGreaterThanZero,
                    "pageIndex");
            }
            if (pageCount <= 0)
            {
                throw new ArgumentException(
                    Resources.Messages.exception_PageCountShouldGreaterThanZero,
                    "pageCount");
            }
            query = query.Skip(pageIndex * pageCount).Take(pageCount);
            return query;
        }
    }
}
