using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using NetTopologySuite.Features;

namespace Core.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Add<T>(this IEnumerable<T> enumerable, T value)
        {
            foreach (var cur in enumerable)
            {
                yield return cur;
            }

            yield return value;
        }

        public static IEnumerable<T> AddRange<T>(this IEnumerable<T> enumerable, IEnumerable<T> values)
        {
            foreach (var cur in enumerable)
            {
                yield return cur;
            }

            foreach (var cur in values)
            {
                yield return cur;
            }
        }


        public static async Task<FeatureCollection> ToFeatureCollection<T>(this IAsyncEnumerable<T> entities) where T : EntityGeo
        {
            var features = ConvertToCollection(entities);
            var featureCollection = new FeatureCollection();

            await foreach (var item in features)
            {
                featureCollection.Add(item);
            }

            return featureCollection;
        }


        private static async IAsyncEnumerable<IFeature> ConvertToCollection(this IAsyncEnumerable<EntityGeo> entitysGeo)
        {
            await foreach (var entityGeo in entitysGeo)
            {
                yield return (Feature)entityGeo;
            }
        }

        /// <summary>
        /// se a 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="enumerable"></param> 
        /// <param name="methodWhenSome"></param>
        /// <param name="methodWhenNone"></param>
        /// <returns></returns>
        public static TResult Math<T, TResult>(this IEnumerable<T> enumerable,
            Func<IEnumerable<T>, TResult> methodWhenSome,
            Func<TResult> methodWhenNone)
            => enumerable.Any() ? methodWhenSome(enumerable) : methodWhenNone();
    }
}