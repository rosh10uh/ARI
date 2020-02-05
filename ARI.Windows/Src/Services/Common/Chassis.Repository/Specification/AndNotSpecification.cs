using System;
using System.Linq.Expressions;

namespace Chassis.Repository.Specification
{
    /// <summary>
    /// Used to validate that left expression is not equal to right expression.
    /// </summary>
    /// <typeparam name="T">Model will be passed here.</typeparam>
    public class AndNotSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _leftSpecification;
        private readonly Specification<T> _rightSpecification;

        /// <summary>
        /// Injects specification and initialize the properties
        /// </summary>
        /// <param name="leftSpecification">To satisfied left specification expression</param>
        /// <param name="rightSpecification">To satisfied right specification expression</param>
        public AndNotSpecification(Specification<T> leftSpecification, Specification<T> rightSpecification)
        {
            _leftSpecification = leftSpecification;
            _rightSpecification = rightSpecification;
        }

        /// <summary>
        /// It will check that left expression is true and right expression is false.
        /// Both expression should not be equal.
        /// </summary>
        /// <returns>Expression is valid or not.</returns>
        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> leftExpression = _leftSpecification.ToExpression();
            Expression<Func<T, bool>> rightExpression = _rightSpecification.ToExpression();
            UnaryExpression andNotleft = UnaryExpression.IsTrue(leftExpression.Body);
            UnaryExpression andNotRight = UnaryExpression.IsFalse(rightExpression.Body);
            var andNotExpressionCheck = Expression.AndAlso(andNotleft, andNotRight);
            if (!ReferenceEquals(leftExpression.Parameters[0], rightExpression.Parameters[0]))
            {
                andNotExpressionCheck = Expression.NotEqual(leftExpression.Body, Expression.Invoke(rightExpression, leftExpression.Parameters[0]));
            }
            return Expression.Lambda<Func<T, bool>>(andNotExpressionCheck, leftExpression.Parameters[0]);
        }
    }
}
