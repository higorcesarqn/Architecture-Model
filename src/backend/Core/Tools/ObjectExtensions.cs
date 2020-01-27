using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Tools
{
    public static class ObjectExtensions
    {
        public static IDictionary<string, object> ToDictionary<T>(this T obj, params Expression<Func<T, object>>[] remover)
        {
            var type = obj.GetType();

            var r = remover.Select((x) =>
            {
                var expression = (MemberExpression)x.Body;
                return expression.Member.Name;
            });

            return type
                        .GetProperties()
                        .Where(x => !r.Contains(x.Name))
                        .ToDictionary(key => key.Name, value => value.GetValue(obj));
        }

        public static IDictionary<string, string> ToDictionaryString<T>(this T obj, params Expression<Func<T, object>>[] remover)
        {
            var type = obj.GetType();

            var r = remover.Select((x) =>
            {
                var expression = (MemberExpression)x.Body;
                return expression.Member.Name;
            });

            return type
                        .GetProperties()
                        .Where(x => !r.Contains(x.Name))
                        .ToDictionary(key => key.Name, value => value.GetValue(obj)?.ToString());
        }
    }
}