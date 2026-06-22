using PrimeShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.Common.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        IUnitOfWork UnitOfWork { get; }
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T?> GetAsync(Guid id);
        IQueryable<T> GetAll();

        Task<IEnumerable<TResult>> QuerySql<TResult>(string sql, object parameters = null);

    }
}
