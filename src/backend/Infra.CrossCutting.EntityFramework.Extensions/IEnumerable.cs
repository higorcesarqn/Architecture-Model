using System;
using System.Collections.Generic;
using System.Linq;

//namespace Microsoft.EntityFrameworkCore
namespace Infra.EntityFramework.Extensions
{
    public static class IEnumerable
    {
        public static IEnumerable<TSource> Distinct<TSource, TCompare>(
             this IEnumerable<TSource> source, Func<TSource, TCompare> selector)
        {
            return source.Distinct(new EqualityComparer<TSource, TCompare>(selector));
        }

        public static IEnumerable<TSource> Intersect<TSource, TCompare>(
             this IEnumerable<TSource> source, IEnumerable<TSource> intersect, Func<TSource, TCompare> selector)
        {
            return source.Intersect(intersect, new EqualityComparer<TSource, TCompare>(selector));
        }

        public static IEnumerable<TSource> Except<TSource, TCompare>(
             this IEnumerable<TSource> source, IEnumerable<TSource> except, Func<TSource, TCompare> selector)
        {
            return source.Except(except, new EqualityComparer<TSource, TCompare>(selector));
        }
    }
}