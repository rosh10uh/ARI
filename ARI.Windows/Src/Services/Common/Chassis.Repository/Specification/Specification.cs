using System;
using System.Linq.Expressions;
using Chassis.Repository.Interfaces;

namespace Chassis.Repository.Specification
{
    /// <summary>
    /// This class implements ISpecification.
    /// It is used for validating the methods of Specification that is And, Or, AndNot, OrNot and Not.
    /// </summary>
    /// <typeparam name="T">Model will be passed here.</typeparam>
    public abstract class Specification<T> : ISpecification<T>
    {
        /// <summary>
        /// Abstract method of type Expression which represents strongly typed lambda expression.
        /// </summary>
        /// <returns>Return boolean.</returns>
        public abstract Expression<Func<T, bool>> ToExpression();

        /// <summary>
        /// To compile expression and satisfy the condition.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }

        /// <summary>
        /// To check equality of the expression.
        /// </summary>
        /// <param name="specification">Pass Ispecification parameter for AndSpecification</param>
        /// <returns>It will return that AndSpecification is satiesfied or not.</returns>
        public Specification<T> And(Specification<T> specification)
        {
            return new AndSpecification<T>(this, specification);
        }

        /// <summary>
        /// To check any of the expression is true in specification. 
        /// </summary>
        /// <param name="specification">To check expression for Or</param>
        /// <returns>Or expression is satiesfied or not.</returns>
        public Specification<T> Or(Specification<T> specification)
        {
            return new OrSpecification<T>(this, specification);
        }

        /// <summary>
        /// To check inequality.
        /// </summary>
        /// <param name="specification">Not expression will be checked</param>
        /// <returns>It will return that NotSpecification is satiesfied or not.</returns>
        public Specification<T> Not(Specification<T> specification)
        {
            return new NotSpecification<T>(this, specification);
        }

        /// <summary>
        /// To check expression with AndNot as true or false.
        /// </summary>
        /// <param name="specification"></param>
        /// <returns>Satiesfied AndNot expression.</returns>
        public Specification<T> AndNot(Specification<T> specification)
        {
            return new AndNotSpecification<T>(this, specification);
        }

        /// <summary>
        /// To check expression with OrNot condition which is true or false.
        /// </summary>
        /// <param name="specification"></param>
        /// <returns>Satiesfied OrNot expression.</returns>
        public Specification<T> OrNot(Specification<T> specification)
        {
            return new OrNotSpecificaton<T>(this, specification);
        }
    }
}
