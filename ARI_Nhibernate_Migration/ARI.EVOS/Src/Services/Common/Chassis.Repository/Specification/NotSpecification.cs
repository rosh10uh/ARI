using System;
using System.Linq.Expressions;

namespace Chassis.Repository.Specification
{
    /// <summary>
    /// This class is used for inequality comparision.
    /// </summary>
    /// <typeparam name="T">Model will be passed here.</typeparam>
    public class NotSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _leftSpecification;
        private readonly Specification<T> _rightSpecification;

        /// <summary>
        /// Constructor injects ISpecification interface.
        /// </summary>
        /// <param name="leftSpecification">To satisfied left specification expression</param>
        /// <param name="rightSpecification">To satisfied right specification expression</param>
        public NotSpecification(Specification<T> leftSpecification, Specification<T> rightSpecification)
        {
            _leftSpecification = leftSpecification;
            _rightSpecification = rightSpecification;
        }

        /// <summary>
        /// This method is used to satisfied the conditions.
        /// Expression is a class which represents a strongly typed lambda expression as a data structure in the form of an expression tree.
        /// BinaryExpression class represents an expression that has a binary operator.
        /// </summary>
        /// <returns>Both expressions are same for specified value or not.</returns>
        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> leftExpression = _leftSpecification.ToExpression();
            Expression<Func<T, bool>> rightExpression = _rightSpecification.ToExpression();
            BinaryExpression notExpression = Expression.NotEqual(leftExpression.Body, rightExpression.Body);
            if (!ReferenceEquals(leftExpression.Parameters[0], rightExpression.Parameters[0]))
            {
                notExpression = Expression.NotEqual(leftExpression.Body, Expression.Invoke(rightExpression, leftExpression.Parameters[0]));
            }
            return Expression.Lambda<Func<T, bool>>(notExpression, leftExpression.Parameters[0]);
        }
    }
}
