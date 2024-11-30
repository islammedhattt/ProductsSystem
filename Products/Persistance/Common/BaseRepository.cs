using Application.Common.Repositories;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Common
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DBContext Context;

        public BaseRepository(DBContext context)
        {
            Context = context;
        }

        public void Create(T entity)
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
            Context.Add(entity);
        }

        public void Delete(T entity)
        {
            entity.UpdatedAt = DateTime.Now;
            entity.IsActive = false;
            Context.Update(entity);
        }

        public Task<List<T>> GetAll(CancellationToken cancellationToken)
        {
            return Context.Set<T>().Where(t => t.IsActive == true).ToListAsync(cancellationToken);
        }

        public async Task<T> GetById(Guid Id, CancellationToken cancellationToken)
        {
            return await Context.Set<T>().FirstAsync(t => t.Id == Id, cancellationToken);
        }

      

        public void Update(T entity)
        {
            entity.UpdatedAt = DateTime.Now;
            Context.Update(entity);
        }
    }
}
