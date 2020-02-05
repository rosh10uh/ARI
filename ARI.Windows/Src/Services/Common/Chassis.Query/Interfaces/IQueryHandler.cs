using System.Data;
using CSharpFunctionalExtensions;

namespace Chassis.Query.Interfaces
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Maybe<TResult> Handle(TQuery query, bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text);
    }

    public interface IQueryHandler<in TQuery, T1, T2> where TQuery : IQuery<T1, T2>
    {
        (Maybe<T1>, Maybe<T2>) Handle(TQuery query, bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text);
    }

    public interface IQueryHandler<in TQuery, T1, T2, T3> where TQuery : IQuery<T1, T2, T3>
    {
        (Maybe<T1>, Maybe<T2>, Maybe<T3>) Handle(TQuery query, bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text);
    }

    public interface IQueryHandler<in TQuery, T1, T2, T3, T4> where TQuery : IQuery<T1, T2, T3, T4>
    {
        (Maybe<T1>, Maybe<T2>, Maybe<T3>, Maybe<T4>) Handle(TQuery query, bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text);
    }

    public interface IQueryHandler<in TQuery, T1, T2, T3, T4, T5> where TQuery : IQuery<T1, T2, T3, T4, T5>
    {
        (Maybe<T1>, Maybe<T2>, Maybe<T3>, Maybe<T4>, Maybe<T5>) Handle(TQuery query, bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text);
    }
}
