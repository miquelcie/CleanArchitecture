using Customers.Core.Entities.Base;
using Customers.Core.Repositories.Base;
using Customers.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly CustomerContext _customerContext;

        public Repository(CustomerContext customerContext)
        {
            _customerContext = customerContext;
        }
        public async Task<T> AddAsync(T entity)
        {
            _customerContext.Set<T>().Add(entity);
            await _customerContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _customerContext.Entry(entity).State = EntityState.Deleted;
            await _customerContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _customerContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _customerContext.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _customerContext.Entry(entity).State= EntityState.Modified;
            await _customerContext.SaveChangesAsync();
            
        }
    }
}
