using System;
using Core.PagedList;

namespace Core.Extensions
{
    public static class IPagedListExtensions
    {
        public static TResult Match<T,TResult>(this IPagedList<T> pagedList,
            Func<IPagedList<T>, TResult> methodWhenSome,
            Func<TResult> methodWhenNone)
        {
            return pagedList.TotalCount > 0 ? methodWhenSome(pagedList)
                       : methodWhenNone();
        }
    }
}
