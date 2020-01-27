﻿using System;
using System.Collections.Generic;

namespace Tango.Linq
{
    /// <summary>
    /// A class to build equality comparer to any type by demand.
    /// </summary>
    /// <typeparam name="T">The type of objects to compare.This type parameter is contravariant. 
    /// <para>That is, you can use either the type you specified or any type that is less derived. </para>
    /// </typeparam>
    public class EqualityComparerBuilder<T> : IEqualityComparer<T>
    {
        /// <summary>
        /// Method used by <see cref="IEqualityComparer{T}.Equals(T, T)"/> of the <see cref="IEqualityComparer{T}"/> interface.
        /// </summary>
        public Func<T, T, bool> Comparer { get; }
        /// <summary>
        /// Method used by <see cref="IEqualityComparer{T}.GetHashCode(T)"/> of the <see cref="IEqualityComparer{T}"/> interface.
        /// </summary>
        public Func<T, int> HashCodeGetter { get; }

        private EqualityComparerBuilder(Func<T, T, bool> comparer, Func<T, int> hashCodeGetter)
        {
            Comparer = comparer;
            HashCodeGetter = hashCodeGetter;
        }

        /// <summary>
        /// Creates a new object that implements <see cref="IEqualityComparer{T}"/> interface.
        /// </summary>
        /// <param name="comparer">Method used by <see cref="IEqualityComparer{T}.Equals(T, T)"/> of the <see cref="IEqualityComparer{T}"/> interface.</param>
        /// <param name="hashCodeGetter">Method used by <see cref="IEqualityComparer{T}.GetHashCode(T)"/> of the <see cref="IEqualityComparer{T}"/> interface.</param>
        /// <returns>Returns an object that implements <see cref="IEqualityComparer{T}"/> interface with the methods <paramref name="comparer"/> and <paramref name="hashCodeGetter"/>.</returns>
        public static EqualityComparerBuilder<T> Create(Func<T, T, bool> comparer, Func<T, int> hashCodeGetter)
            => new EqualityComparerBuilder<T>(comparer, hashCodeGetter);

        /// <summary>
        /// Interface method that invokes the <see cref="Comparer"/> property to resolve the equality between two values.
        /// </summary>
        /// <param name="x">first input value.</param>
        /// <param name="y">second input value.</param>
        /// <returns>
        /// Returns the result of <see cref="Comparer"/> method with two input values.
        /// </returns>
        public bool Equals(T x, T y)
            => Comparer(x, y);

        /// <summary>
        /// Interface method that invokes the <see cref="HashCodeGetter"/> property to get the hash code of the object.
        /// </summary>
        /// <param name="obj">input value.</param>
        /// <returns>Hash code generated by <see cref="HashCodeGetter"/> method.</returns>
        public int GetHashCode(T obj)
            => HashCodeGetter(obj);
    }
}
