using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;
using Domain;

namespace Application.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> Create(T entity, CancellationToken cancellationToken = default);
        Task<T> Update(T entity, CancellationToken cancellationToken = default);
        ValueTask<T> GetById(Guid id, CancellationToken cancellationToken = default);
        Task Delete(T entity, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<T>> ListAll(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<T>> List(ISpecification<T> spec, CancellationToken cancellationToken = default);
        Task<int> Count(ISpecification<T> spec, CancellationToken cancellationToken = default);
    }
}