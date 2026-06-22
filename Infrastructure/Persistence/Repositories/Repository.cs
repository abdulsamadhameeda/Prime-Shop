using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Domain.Common;
using System.Data;
using Dapper;

namespace PrimeShop.Infrastructure.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbConnection _connection;
        public IUnitOfWork UnitOfWork { get; }

        public Repository(ApplicationDbContext context , IDbConnection connection)
        {
            _context = context;
            _connection = connection;

            UnitOfWork = new UnitOfWork(_context);
        }

        public void Add(T entity) 
        { 
            _context.Set<T>().Add(entity); 
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<T?> GetAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }


        public async Task<IEnumerable<TResult>> QuerySql<TResult>(string sql, object parameters = null)
        {
            return await _connection.QueryAsync<TResult>(sql, parameters);
        }

    }
}
