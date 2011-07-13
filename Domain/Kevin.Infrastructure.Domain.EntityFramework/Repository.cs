using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Kevin.Infrastructure.Domain.EntityFramework
{

    using Extensions;

    public abstract class Repository<T, TId> : IRepository<T, TId>
        where T : EntityBase<TId>, IAggregateRoot
    {

        #region Members

        protected IEntityUnitOfWork UnitOfWork
        {
            get
            {
                return _unitOfWork;
            }
        }
        private IEntityUnitOfWork _unitOfWork;

        protected DbQuery<T> Query
        {
            get
            {
                var query = _unitOfWork.Query<T, TId>();
                Include<T>(query);
                return query;
            }
        }

        #endregion

        #region Constructor

        public Repository(IEntityUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }

            _unitOfWork = unitOfWork;
        }

        #endregion

        #region IRepository<T, TId> Implementation

        public virtual void Add(T entity)
        {
            _unitOfWork.RegisterNew<T, TId>(entity);
        }

        public virtual void Update(T entity)
        {
            _unitOfWork.RegisterModify<T, TId>(entity);
        }

        public virtual void Remove(T entity)
        {
            _unitOfWork.RegisterRemove<T, TId>(entity);
        }

        public virtual IEnumerable<T> FindAll()
        {
            return Query.ToArray();
        }

        public virtual IEnumerable<T> FindBy(Specification.ISpecification<T> specification, params ListSortDescription[] sortDescriptions)
        {
            var query = Query as IQueryable<T>;
            query = query.FindBy<T, TId>(specification);

            return query.ToArray();
        }

        public virtual IEnumerable<T> FindPageBy(Specification.ISpecification<T> specification, int pageIndex, int pageCount, params ListSortDescription[] sortDescriptions)
        {

            var query = Query as IQueryable<T>;
            query = query
                .FindBy<T, TId>(specification)
                .Sort<T, TId>(sortDescriptions)
                .Paginate(pageIndex, pageCount);
            return query.ToArray();
        }

        public virtual IEnumerable<T> FindPageBy(Specification.ISpecification<T> specification,
            int pageIndex, int pageCount, out int totalCount,
            params ListSortDescription[] sortDescriptions)
        {
            var query = Query as IQueryable<T>;
            query = query.FindBy<T, TId>(specification);
            //获取过滤后的记录总数
            totalCount = query.Count();
            query = query.Sort<T, TId>(sortDescriptions).Paginate(pageIndex, pageCount);
            return query.ToArray();
        }

        #endregion

        #region Methods

        protected virtual void Include<T>(DbQuery<T> query)
        {
        }

        #endregion

    }
}
