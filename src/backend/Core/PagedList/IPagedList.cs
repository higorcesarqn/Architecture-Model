// Copyright (c) Arch team. All rights reserved.

using System.Collections.Generic;

namespace Core.PagedList
{
    /// <summary>
    /// Provides the interface(s) for paged list of any type.
    /// </summary>
    /// <typeparam name="TResult">The type for paging.</typeparam>
    public interface IPagedList<TResult>
    {
        /// <summary>
        /// Gets the index start value.
        /// </summary>
        /// <value>The index start value.</value>
        int IndexFrom { get; }

        /// <summary>
        /// Gets the page index (current).
        /// </summary>
        int PageIndex { get; }

        /// <summary>
        /// Gets the page size.
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// Gets the total count of the list of type <typeparamref name="TResult"/>
        /// </summary>
        int TotalCount { get; }

        /// <summary>
        /// Gets the total pages.
        /// </summary>
        int TotalPages { get; }

        /// <summary>
        /// Gets the current page items.
        /// </summary>
        IList<TResult> Items { get; }

        /// <summary>
        /// Gets the has previous page.
        /// </summary>
        /// <value>The has previous page.</value>
        bool HasPreviousPage { get; }

        /// <summary>
        /// Gets the has next page.
        /// </summary>
        /// <value>The has next page.</value>
        bool HasNextPage { get; }

       

    }
}