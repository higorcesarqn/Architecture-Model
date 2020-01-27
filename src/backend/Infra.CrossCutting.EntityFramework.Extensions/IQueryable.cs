using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

//namespace Microsoft.EntityFrameworkCore
namespace Infra.EntityFramework.Extensions
{
    public static class IQueryable
    {
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes)
            where T : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query,
                        (current, include) => current.Include(include));
            }

            return query;
        }

        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params string[] includes)
            where T : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }
            return query;
        }

        public static IQueryable<TSource> Distinct<TSource, TCompare>(
          this IQueryable<TSource> source, Func<TSource, TCompare> selector)
        {
            return source.Distinct(new EqualityComparer<TSource, TCompare>(selector));
        }

        public static IQueryable<TSource> Intersect<TSource, TCompare>(
             this IQueryable<TSource> source, IQueryable<TSource> intersect, Func<TSource, TCompare> selector)
        {
            return source.Intersect(intersect, new EqualityComparer<TSource, TCompare>(selector));
        }

        public static IQueryable<TSource> Except<TSource, TCompare>(
            this IQueryable<TSource> source, IQueryable<TSource> Except, Func<TSource, TCompare> selector)
        {
            return source.Except(Except, new EqualityComparer<TSource, TCompare>(selector));
        }

    }
}