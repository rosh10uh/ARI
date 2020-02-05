using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Chassis.Command.ExtensionMethods
{
    /// <summary>
    /// This static class is used to convert type of IResult to Result
    /// </summary>
    public static class ResultExtension
    {
        /// <summary>
        /// Extension method to used for get result of type T class
        /// </summary>
        /// <typeparam name="T">Pass T as a type of class</typeparam>
        /// <param name="result">Pass result</param>
        /// <returns>It returns a result of type T if isSuccess is true</returns>
        public static Task<Result<T>> GetResult<T>(this IResult result)
        {
            return ((Task<Result<T>>)result);
        }

        /// <summary>
        /// Extension method to used for get result of type T class
        /// </summary>
        /// <param name="result">Pass result</param>
        /// <returns>It returns a result without any type</returns>
        public static Task<Result> GetResult(this IResult result)
        {
            return ((Task<Result>)result);
        }
    }
}
