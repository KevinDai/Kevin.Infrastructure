using System;
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

        internal static IQueryable<T> Sort<T, TId>(this IQueryable<T> query, params ListSortDescription[] sortDescriptions) where T : EntityBase<TId>
        {
            if (sortDescriptions.Any())
            {
                Expression<Func<T, object>> exp = t => t.Id;
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
            query = query.Skip(pageIndex * pageCount).Take(pageCount);
            return query;
        }
    }
}
