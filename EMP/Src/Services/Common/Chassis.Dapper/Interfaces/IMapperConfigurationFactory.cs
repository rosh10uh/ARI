using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace Chassis.Dapper.Interfaces
{
    internal interface IMapperConfigurationFactory
    {
        void CreateMap(Type source, Type destination);
        void CreateMap(Maybe<IEnumerable<object>>[] sourceObj, Type[] destination);
        TReturn Map<TReturn>(IEnumerable<object> value);
    }
}
