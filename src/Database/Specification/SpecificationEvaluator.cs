using System.Linq;
using Application.Specification;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Database.Specification
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> specification)
        {
            if (specification.Criteria is not null)
            {
                query = query.Where(specification.Criteria);
            }

            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));
            
            query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));
            
            if (specification.OrderBy is not null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending is not null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.GroupBy is not null)
            {
                query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
            }
            
            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip)
                    .Take(specification.Take);
            }
            
            return query;
        }
    }
}