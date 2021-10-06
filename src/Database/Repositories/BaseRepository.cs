using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Repositories;
using Application.Specification;
using Database.Specification;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> Create(T entity, CancellationToken cancellationToken = default)
        {
            await _context.Set<T>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task<T> Update(T entity, CancellationToken cancellationToken = default)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public ValueTask<T> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            return _context.Set<T>().FindAsync(id, cancellationToken);
        }

        public Task Delete(T entity, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Remove(entity);
            return _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<T>> ListAll(CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<T>> List(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.ToListAsync(cancellationToken);
        }

        public Task<int> Count(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            return ApplySpecification(spec).CountAsync(cancellationToken);
        }
        
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}