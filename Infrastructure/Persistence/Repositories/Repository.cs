using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Infrastructure.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly ApplicationDbContext _context;
        public IUnitOfWork UnitOfWork { get; }

        public Repository(ApplicationDbContext context)
        {
            _context = context;

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
    }
}
