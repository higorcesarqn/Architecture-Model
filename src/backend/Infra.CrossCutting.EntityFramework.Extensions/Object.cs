using System.Linq;

namespace Infra.EntityFramework.Extensions
{
    public static class Object
    {
        public static string ObjectToUri<T>(this T obj)
        {
            return string.Join("&", obj.GetType().GetProperties().Select(p => p.Name + "=" + p.GetValue(obj, null)));
        }

        public static bool CompareValues<T>(this T source, T comparer)
        {
            return source.GetType().GetProperties().Any(x => x.GetValue(source).Equals(comparer.GetType().GetProperty(x.Name)?.GetValue(comparer)));
        }
    }
}