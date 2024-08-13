using HepsiApi.Application.Interfaces.Repositories;
using HepsiApi.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiApi.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : class, IEntityBase, new()
    {
        private readonly DbContext dbContext;

        public WriteRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        private DbSet<T> Table { get => dbContext.Set<T>(); }
        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
        }

        public async Task AddRangeAsync(IList<T> entities)
        {
            await Table.AddRangeAsync(entities);
        }
        //update işlemleri async olarak gerçekleşmiyor
        public async Task<T> UpdateAsync(T entity)
        {
            await Task.Run(() => Table.Update(entity)); 
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(()=>Table.Remove(entity));
        }




    }
}
