using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Kevin.Infrastructure.Domain.EntityFramework
{
    public interface IEntityUnitOfWork : IUnitOfWork
    {
        DbQuery<T> Query<T, TId>() where T : EntityBase<TId>;
    }
}
