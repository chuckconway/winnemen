using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Winnemen.Core.Extensions
{
    public static class MapperExtensions
    {
        public static TDestination Map<TSource, TDestination>(this TSource source)
            where TSource : class
            where TDestination : class
        {
            return Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination Map<TSource, TDestination>(this TSource source, TDestination destination)
            where TSource : class
            where TDestination : class
        {
            return Mapper.Map(source, destination);
        }
    }
}
