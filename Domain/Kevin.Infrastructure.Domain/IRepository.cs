using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kevin.Infrastructure.Domain
{

    using Specification;

    public interface IRepository<TEntity, TId>
        where TEntity : EntityBase<TId>, IAggregateRoot
    {

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);

        IEnumerable<TEntity> FindAll();

        IEnumerable<TEntity> FindBy(ISpecification<TEntity> specification, params ListSortDescription[] sortDescriptions);

        IEnumerable<TEntity> FindPageBy(ISpecification<TEntity> specification, int pageIndex, int pageCount, params ListSortDescription[] sortDescriptions);

        IEnumerable<TEntity> FindPageBy(ISpecification<TEntity> specification, int pageIndex, int pageCount, out int totalCount, params ListSortDescription[] sortDescriptions);
    }
}
