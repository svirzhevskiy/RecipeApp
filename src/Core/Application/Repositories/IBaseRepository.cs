using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Application.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> Create(T entity);
        Task<IEnumerable<T>> GetAll(int take, int skip, Func<bool, T> filter);
        Task<T> Update(T entity);
        Task<T> GetById(Guid id);
        Task Delete(Guid id);
    }
}