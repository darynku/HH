using System.Linq.Expressions;
using HH.Application.Specifications.Abstraction;

namespace HH.Application.Specifications
{
    public class Specification<T> : ISpecification<T>
    {
        public Specification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; }
    }
}
