using AutoMapper;
using CSharpFunctionalExtensions;
using Chassis.Dapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chassis.Dapper.Utility
{
    internal class MapperConfigurationFactory : IMapperConfigurationFactory
    {
        private IMapper _mapper;
        private readonly QueryProfiler _queryProfiler;
        public MapperConfigurationFactory()
        {
            _queryProfiler = new QueryProfiler();
            var configurationProvider = new MapperConfiguration(cfg => cfg.AddProfile(_queryProfiler));
            _mapper = configurationProvider.CreateMapper();
        }

        public void CreateMap(Type source, Type destination)
        {
            _queryProfiler.AddMap(source, destination);
        }

        public TReturn Map<TReturn>(IEnumerable<object> value)
        {
            TReturn @return;
            if (typeof(TReturn).IsGenericType)
                @return = _mapper.Map<TReturn>(value);
            else
                @return = _mapper.Map<TReturn>(value?.FirstOrDefault());

            return @return;
        }

        public void CreateMap(Maybe<IEnumerable<object>>[] sourceObj, Type[] destination)
        {
            for (int index = 0; index < destination.Length; index++)
            {
                _queryProfiler.AddMap(sourceObj.ElementAt(0).GetType(), destination[0]);
            }
        }
    }

    public class QueryProfiler : Profile
    {
        private IDictionary<Type, Type> _mapperTypes = new Dictionary<Type, Type>();
        public void AddMap(Type source, Type destination)
        {
            //if (!_mapperTypes.Contains(new KeyValuePair<Type, Type>(source, destination)))
            //{
            //    CreateMap(source, destination);
            //    _mapperTypes.Add(source, destination);
            //}
        }
    }

   
}
