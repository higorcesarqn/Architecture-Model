using System;
using System.Collections.Generic;

//namespace Microsoft.EntityFrameworkCore
namespace Infra.EntityFramework.Extensions
{
    public class EqualityComparer<TSource, TDest> : IEqualityComparer<TSource>
    {
        private Func<TSource, TDest> _selector;

        public EqualityComparer(Func<TSource, TDest> selector)
        {
            _selector = selector;
        }

        public bool Equals(TSource obj, TSource other)
        {
            return _selector(obj).Equals(_selector(other));
        }

        public int GetHashCode(TSource obj)
        {
            return _selector(obj).GetHashCode();
        }
    }
}