using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nCoVMSApi.Common
{
    public class MapperHelper
    {
        public static TDestination MapperTo<TSource, TDestination>(TSource source)
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = mapperConfig.CreateMapper();
            return mapper.Map<TSource, TDestination>(source);
        }

        public static List<TDestination> MapperToList<TSource, TDestination>(List<TSource> source)
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = mapperConfig.CreateMapper();
            return mapper.Map<List<TSource>, List<TDestination>>(source);
        }
    }
}
