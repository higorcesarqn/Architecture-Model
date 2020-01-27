using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Enuns

{
    public abstract class EnumTools
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static IEnumerable<string> GetDescriptions<T>()
        {
            var values = GetValues<T>();
            foreach (var value in values)
            {
                yield return value.ToString();
            }
        }
    }
}