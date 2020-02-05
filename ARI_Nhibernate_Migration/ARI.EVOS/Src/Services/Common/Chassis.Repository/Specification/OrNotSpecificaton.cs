using System;
using System.Linq.Expressions;

namespace Chassis.Repository.Specification
{
    /// <summary>
    /// To validate OrNot condition for expression.
    /// </summary>
    /// <typeparam name="T">Model will be passed here.</typeparam>
    public class OrNotSpecificaton<T> : Specification<T>
    {
        private readonly Specification<T> _leftSpecification;
        private readonly Specification<T> _rightSpecification;

        /// <summary>
        /// Injects Specification class of type T.
        /// </summary>
        /// <param name="leftSpecification">To satisfied left specification expression</param>
        /// <param name="rightSpecification">To satisfied right specification expression</param>
        public OrNotSpecificaton(Specification<T> leftSpecification, Specification<T> rightSpecification)
        {
            _leftSpecification = leftSpecification;
            _rightSpecification = rightSpecification;
        }

        /// <summary>
        /// To satisfy the condition of left expression and for right expression is true or not.
        /// </summary>
        /// <returns>Expression is valid or not.</returns>
        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> leftExpression = _leftSpecification.ToExpression();
            Expression<Func<T, bool>> rightExpression = _rightSpecification.ToExpression();
            UnaryExpression orNotLeft = UnaryExpression.IsTrue(leftExpression.Body);
            UnaryExpression orNotRight = UnaryExpression.IsFalse(rightExpression.Body);
            var orNotExpressionCheck = Expression.OrElse(orNotLeft, orNotRight);
            if (!ReferenceEquals(leftExpression.Parameters[0], rightExpression.Parameters[0]))
            {
                orNotExpressionCheck = Expression.Equal(leftExpression.Body, Expression.Invoke(rightExpression, leftExpression.Parameters[0]));
            }
            return Expression.Lambda<Func<T, bool>>(orNotExpressionCheck, leftExpression.Parameters[0]);
        }
    }
}
