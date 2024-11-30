using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        void Create(T entity);
        Task<T> GetById(Guid Id, CancellationToken cancellationToken);
        void Update(T entity);
        void Delete(T entity);
        Task<List<T>> GetAll(CancellationToken cancellationToken);
    }
}
