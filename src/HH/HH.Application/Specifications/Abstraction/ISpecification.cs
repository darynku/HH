using System.Linq.Expressions;

namespace HH.Application.Specifications.Abstraction
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
    }
}