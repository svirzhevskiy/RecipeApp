using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Application.Specification
{
    public class Specification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; } = null!;

        public Specification()
        {
            
        }

        public Specification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public List<Expression<Func<T, object>>> Includes { get; } = new();
        public List<string> IncludeStrings { get; } = new();
        public Expression<Func<T, object>> OrderBy { get; private set; } = null!;
        public Expression<Func<T, object>> OrderByDescending { get; private set; } = null!;
        public Expression<Func<T, object>> GroupBy { get; private set; } = null!;
        
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; } = false;

        protected virtual void AddInclude(Expression<Func<T, object>> expression)
            => Includes.Add(expression);

        protected virtual void AddInclude(string include)
            => IncludeStrings.Add(include);

        protected virtual void ApplyPaging(int skip, int take) 
            => (Skip, Take, IsPagingEnabled) = (skip, take, true);

        protected virtual void ApplyOrderBy(Expression<Func<T, object>> expression)
            => OrderBy = expression;

        protected virtual void ApplyOrderByDescending(Expression<Func<T, object>> expression)
            => OrderByDescending = expression;

        protected virtual void ApplyGroupBy(Expression<Func<T, object>> expression)
            => GroupBy = expression;
    }
}