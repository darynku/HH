namespace HH.Application.Specifications.Extension
{
    using System;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    public static class SpecificationExtensions
    {
        public static Specification<T> And<T>(this Specification<T> first, Specification<T> second)
        {
            return new Specification<T>(first.Criteria.AndAlso(second.Criteria));
        }


        private static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            var parameter = Expression.Parameter(typeof(T));
            var firstBody = new ReplaceExpressionVisitor(first.Parameters[0], parameter).Visit(first.Body);
            var secondBody = new ReplaceExpressionVisitor(second.Parameters[0], parameter).Visit(second.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(firstBody, secondBody), parameter);
         }
    }

}
