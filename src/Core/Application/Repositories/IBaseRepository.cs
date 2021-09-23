using System;
using System.Threading.Tasks;
using Domain;

namespace Application.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<T> GetById(Guid id);
        Task Delete(Guid id);
    }
}