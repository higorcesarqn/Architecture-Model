﻿using System;
using Tango.Functional;

namespace Tango.Types
{
    /// <summary>
    /// Represents a value of one of two possible types, similar to Either values. 
    /// Instances of <see cref="Continuation{TFail,TSuccess}"/> contains an instance of <typeparamref name="TSuccess"/> or <typeparamref name="TFail"/>.
    /// <para>
    /// A common use of <see cref="Continuation{TFail, TSuccess}"/> is to represents a result of an operation that can be successful or unsuccessful.
    /// This value can be used to implements a Railway Oriented Programming.
    /// </para>
    ///<para>
    /// A <see cref="Continuation{TFail, TSuccess}"/> value is a powerful tool to chaining operations in a sophisticated and idiomatic way dealing
    /// with possible fails without throwing exceptions.
    /// A <see cref="Continuation{TFail, TSuccess}"/> value behaves like a promise of JavaScript, with Then and Catch operations.
    /// </para>
    /// </summary>
    /// <typeparam name="TFail">The type of the fail value</typeparam>
    /// <typeparam name="TSuccess">The type of the success value</typeparam>
    public struct Continuation<TFail, TSuccess>
    {
        private TSuccess Success { get; }
        private TFail Fail { get; }

        /// <summary>
        /// Returns true when the result value is a <see typeparamref="TSuccess"/> value.
        /// Otherwise, returns false.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Returns true when the result value is a <see typeparamref="TFail"/> value.
        /// Otherwise, returns false.
        /// </summary>
        public bool IsFail => !IsSuccess;

        /// <summary>
        /// Initialize a new instance of <see cref="Continuation{TFail,TSuccess}"/> value with a <see typeparamref="TSuccess"/> value.
        /// </summary>
        /// <param name="success">The input <see typeparamref="TSuccess"/> value.</param>
        public Continuation(TSuccess success)
        {
            Success = success;
            Fail = default(TFail);
            IsSuccess = true;
        }

        /// <summary>
        /// Initialize a new instance of <see cref="Continuation{TFail, TSuccess}"/> value with a <see typeparamref="TFail"/> value.
        /// </summary>
        /// <param name="fail">The input <see typeparamref="TFail"/> value.</param>
        public Continuation(TFail fail)
        {
            Fail = fail;
            Success = default(TSuccess);
            IsSuccess = false;
        }

        /// <summary>
        /// Initialize a new instance of <see cref="Continuation{TFail,TSuccess}"/> value with a <see typeparamref="TSuccess"/> value.
        /// </summary>
        /// <param name="success">The input <see typeparamref="TSuccess"/> value.</param>
        public static Continuation<TFail, TSuccess> Return(TSuccess success)
            => new Continuation<TFail, TSuccess>(success);

        /// <summary>
        /// Initialize a new instance of <see cref="Continuation{TFail, TSuccess}"/> value with a <see typeparamref="TFail"/> value.
        /// </summary>
        /// <param name="fail">The input <see typeparamref="TFail"/> value.</param>
        public static Continuation<TFail, TSuccess> Return(TFail fail)
            => new Continuation<TFail, TSuccess>(fail);

        /// <summary>
        /// Initialize a new instance of <see cref="Continuation{TFail, TSuccess}"/> value with a <see typeparamref="TSuccess"/> value.
        /// </summary>
        /// <param name="success">The input <see typeparamref="TSuccess"/> value.</param>
        public static implicit operator Continuation<TFail, TSuccess>(TSuccess success)
            => new Continuation<TFail, TSuccess>(success);

        /// <summary>
        /// Initialize a new instance of <see cref="Continuation{TFail, TSuccess}"/> value with a <see typeparamref="TFail"/> value.
        /// </summary>
        /// <param name="fail">The input <see typeparamref="TFail"/> value.</param>
        public static implicit operator Continuation<TFail, TSuccess>(TFail fail)
            => new Continuation<TFail, TSuccess>(fail);

        /// <summary>
        /// Creates an <see cref="Option{T}"/> of <see typeparamref="TSuccess"/> value by <see cref="Continuation{TSuccess, TFail}"/> value <see cref="Success"/> property.
        /// </summary>
        /// <param name="continuation">The <see cref="Continuation{TFail, TSuccess}"/> value.</param>
        /// <returns>New instance of <see cref="Option{T}"/> value with <see cref="Option{T}.IsSome"/> when the <see cref="Continuation{TSuccess, TFail}"/> value is in <see cref="IsSuccess"/> state.
        /// Otherwise returns <see cref="Option{T}.None"/>
        /// </returns>
        public static implicit operator Option<TSuccess>(Continuation<TFail, TSuccess> continuation)
           => continuation.IsSuccess ?
                continuation.Success
                : Option<TSuccess>.None();

        /// <summary>
        /// Creates an <see cref="Option{T}"/> of <see typeparamref="TFail"/> value by <see cref="Continuation{TSuccess, TFail}"/> value <see cref="Fail"/> property.
        /// </summary>
        /// <param name="continuation">The <see cref="Continuation{TFail, TSuccess}"/> value.</param>
        /// <returns>New instance of <see cref="Option{T}"/> value with <see cref="Option{T}.IsSome"/> when the <see cref="Continuation{TSuccess, TFail}"/> value is in <see cref="IsFail"/> state.
        /// Otherwise returns <see cref="Option{T}.None"/>
        /// </returns>
        public static implicit operator Option<TFail>(Continuation<TFail, TSuccess> continuation)
            => continuation.IsFail ?
                 continuation.Fail
                : Option<TFail>.None();

        /// <summary>
        /// Creates a <see cref="Continuation{TFail, TSuccess}"/> from an <see cref="Either{TLeft, TRight}"/> value
        /// </summary>
        /// <param name="either">input Either value</param>
        /// <returns>
        /// New instance of <see cref="Continuation{TFail, TSuccess}"/> in <see cref="IsFail"/> state when the <paramref name="either"/> <see cref="Either{TLeft, TRight}.IsLeft"/>.
        /// Otherwise returns a <see cref="Continuation{TFail, TSuccess}"/> in <see cref="IsSuccess"/> state.
        /// </returns>
        public static implicit operator Continuation<TFail, TSuccess>(Either<TFail, TSuccess> either)
            => either.Match<Continuation<TFail, TSuccess>>(
                right => right,
                left => left);

        /// <summary>
        /// This allows a sophisticated way to apply some method for <see cref="Continuation{TFail, TSuccess}"/> values without having to check for the existence of a fail or success value.
        /// </summary>
        /// <typeparam name="T">The type of value returned by functions <paramref name="methodWhenFail"/> and <paramref name="methodWhenSuccess"/>.</typeparam>
        /// <param name="methodWhenSuccess">Method that will be invoked when this is in <see cref="IsSuccess"/> state.</param>
        /// <param name="methodWhenFail">Method that will be invoked when this is in <see cref="IsFail"/> state.</param>
        /// <returns>
        /// returns the result of the method according to the <see cref="Continuation{TFail, TSuccess}"/> value.
        /// The returns of <paramref name="methodWhenFail"/> when this is in <see cref="IsFail"/> state, Otherwise the returns of <paramref name="methodWhenSuccess"/>
        /// </returns>
        public T Match<T>(
            Func<TSuccess, T> methodWhenSuccess,
            Func<TFail, T> methodWhenFail)
            => IsFail ?
                methodWhenFail(Fail)
                : methodWhenSuccess(Success);

        /// <summary>
        /// This allows a sophisticated way to apply some action for <see cref="Continuation{TFail, TSuccess}"/> values without having to check for the existence of a fail or success value.
        /// </summary>
        /// <param name="methodWhenSuccess">Method that will be invoked when this is in <see cref="IsSuccess"/> state.</param>
        /// <param name="methodWhenFail">Method that will be invoked when this is in <see cref="IsFail"/> state.</param>
        public Unit Match(
            Action<TSuccess> methodWhenSuccess,
            Action<TFail> methodWhenFail)
            => Match(
                methodWhenSuccess.ToFunction(),
                methodWhenFail.ToFunction()
                );

        /// <summary>
        /// This allows a sophisticated and powerful way to apply some method in order to compose an operation with different functions.
        /// When the current <see cref="Continuation{TFail, TSuccess}"/> is <see cref="IsSuccess"/> the <paramref name="thenMethod"/> is applied.
        /// Otherwise, returns itself until encounter a <see cref="Catch"/> function.
        /// </summary>
        /// <typeparam name="TNewSuccess">The type of the value returned by <paramref name="thenMethod"/>.</typeparam>
        /// <param name="thenMethod">The function to apply when it is in <see cref="IsSuccess"/> state.</param>
        /// <returns>
        /// Returns a new <see cref="Continuation{TFail, TNewSuccess}"/> value when the current value <see cref="IsSuccess"/>.
        /// Otherwise, returns itself.
        /// </returns>
        public Continuation<TFail, TNewSuccess> Then<TNewSuccess>(
            Func<TSuccess, Continuation<TFail, TNewSuccess>> thenMethod)
            => IsSuccess ? thenMethod(Success)
                           : Fail;

        /// <summary>
        /// This allows a sophisticated and powerful way to apply some method in order to compose an operation with different functions.
        /// When the current <see cref="Continuation{TFail, TSuccess}"/> is <see cref="IsSuccess"/> the <paramref name="thenMethod"/> is applied.
        /// Otherwise, returns itself until encounter a <see cref="Catch"/> function.
        /// </summary>
        /// <param name="thenMethod">The function to apply when it is in <see cref="IsSuccess"/> state.</param>
        /// <returns>
        /// Returns a new <see cref="Continuation{TFail, TSuccess}"/> value when the current value <see cref="IsSuccess"/>.
        /// Otherwise, returns itself.</returns>
        public Continuation<TFail, TSuccess> Then(
            Func<TSuccess, Continuation<TFail, TSuccess>> thenMethod)
            => Then<TSuccess>(thenMethod);

        /// <summary>
        /// This allows a sophisticated and powerful way to apply some method in order to compose an operation with different functions.
        /// When the current <see cref="Continuation{TFail, TSuccess}"/> is <see cref="IsSuccess"/> the <paramref name="thenMethod"/> is applied.
        /// Otherwise, returns itself until encounter a <see cref="Catch"/> function.
        /// <para>
        /// In this case, the <paramref name="thenMethod"/> can return just a regular result instead of a <see cref="Continuation{TFail, TSuccess}"/> instance.
        /// </para>
        /// </summary>
        /// <typeparam name="TNewSuccess"></typeparam>
        /// <param name="thenMethod"></param>
        /// <returns></returns>
        public Continuation<TFail, TNewSuccess> Then<TNewSuccess>(
            Func<TSuccess, TNewSuccess> thenMethod)
            => Then(success => Continuation<TFail, TNewSuccess>.Return(thenMethod(success)));

        /// <summary>
        /// This allows a sophisticated and powerful way to apply some method in order to compose an operation with different functions.
        /// When the current <see cref="Continuation{TFail, TSuccess}"/> is <see cref="IsSuccess"/> the <paramref name="thenMethod"/> is applied with the <paramref name="parameter"/> as well.
        /// Otherwise, returns itself until encounter a <see cref="Catch"/> function.
        /// </summary>
        /// <typeparam name="TNewSuccess">The type of the value returned by <paramref name="thenMethod"/>.</typeparam>
        /// <typeparam name="TParameter">The type of parameter value</typeparam>
        /// <param name="thenMethod">The function to apply when it is in <see cref="IsSuccess"/> state.</param>
        /// <param name="parameter">The parameter to apply to <paramref name="thenMethod"/> with current <see cref="Success"/> property.</param>
        /// <returns>
        /// Returns a new <see cref="Continuation{TFail, TNewSuccess}"/> value when the current value <see cref="IsSuccess"/>.
        /// Otherwise, returns itself.</returns>
        public Continuation<TFail, TNewSuccess> Then<TParameter, TNewSuccess>(
            Func<TParameter, TSuccess, Continuation<TFail, TNewSuccess>> thenMethod,
            TParameter parameter)
            => Then((success) => thenMethod(parameter, success));

        /// <summary>
        /// This allows a sophisticated and powerful way to apply some method in order to compose an operation with different functions.
        /// When the current <see cref="Continuation{TFail, TSuccess}"/> is <see cref="IsFail"/> the <paramref name="catchMethod"/> is applied.
        /// Otherwise, returns itself until encounter a <see cref="Then"/> function.
        /// </summary>
        /// <typeparam name="TNewFail">The type of the value returned by <paramref name="catchMethod"/>.</typeparam>
        /// <param name="catchMethod">The function to apply when it is in <see cref="IsFail"/> state.</param>
        /// <returns>
        /// Returns a new <see cref="Continuation{TNewFail, TSuccess}"/> value when the current value <see cref="IsFail"/>.
        /// Otherwise, returns itself.
        /// </returns>
        public Continuation<TNewFail, TSuccess> Catch<TNewFail>(Func<TFail, Continuation<TNewFail, TSuccess>> catchMethod)
            => IsFail ? catchMethod(Fail)
                        : Success;

        /// <summary>
        /// This allows a sophisticated and powerful way to apply some method in order to compose an operation with different functions.
        /// When the current <see cref="Continuation{TFail, TSuccess}"/> is <see cref="IsFail"/> the <paramref name="catchMethod"/> is applied.
        /// Otherwise, returns itself until encounter a <see cref="Then"/> function.
        /// </summary>
        /// <param name="catchMethod">The function to apply when it is in <see cref="IsFail"/> state.</param>
        /// <returns>
        /// Returns a new <see cref="Continuation{TFail, TSuccess}"/> value when the current value <see cref="IsFail"/>.
        /// Otherwise, returns itself.
        /// </returns>
        public Continuation<TFail, TSuccess> Catch(Func<TFail, Continuation<TFail, TSuccess>> catchMethod)
            => Catch<TFail>(catchMethod);

        /// <summary>
        /// This allows a sophisticated and powerful way to apply some method in order to compose an operation with different functions.
        /// When the current <see cref="Continuation{TFail, TSuccess}"/> is <see cref="IsSuccess"/> the <paramref name="thenMethod"/> is applied.
        /// Otherwise, returns itself until encounter a <see cref="Catch"/> function.
        /// </summary>
        /// <param name="first">The continuation itself.</param>
        /// <param name="thenMethod">The function to apply when it is in <see cref="IsSuccess"/> state.</param>
        /// <returns>
        /// Returns a new <see cref="Continuation{TFail, TSuccess }"/> value when the current value <see cref="IsSuccess"/>.
        /// Otherwise, returns itself.</returns>
        public static Continuation<TFail, TSuccess> operator >
            (Continuation<TFail, TSuccess> first,
                Func<TSuccess, Continuation<TFail, TSuccess>> thenMethod)
            => first.Then(thenMethod);

        /// <summary>
        /// Always raises a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="first">The continuation itself.</param>
        /// <param name="thenMethod">The function to apply when it is in <see cref="IsSuccess"/> state.</param>
        /// <returns>
        /// Always raises a <see cref="NotSupportedException"/>.
        /// </returns>
        public static Continuation<TFail, TSuccess> operator <
            (Continuation<TFail, TSuccess> first,
                Func<TSuccess, Continuation<TFail, TSuccess>> thenMethod)
            => throw new NotSupportedException();

        /// <summary>
        /// This allows a sophisticated and powerful way to apply some method in order to compose an operation with different functions.
        /// When the current <see cref="Continuation{TFail, TSuccess}"/> is <see cref="IsSuccess"/> the <paramref name="thenMethod"/> is applied.
        /// Otherwise, returns itself until encounter a <see cref="Catch"/> function.
        /// </summary>
        /// <para>
        /// In this case, the <paramref name="thenMethod"/> can return just a regular result instead of a <see cref="Continuation{TFail, TSuccess}"/> instance.
        /// </para>
        /// <param name="first">The continuation itself.</param>
        /// <param name="thenMethod">The function to apply when it is in <see cref="IsSuccess"/> state.</param>
        /// <returns>
        /// Returns a new <see cref="Continuation{TFail, TSuccess }"/> value when the current value <see cref="IsSuccess"/>.
        /// Otherwise, returns itself.</returns>
        public static Continuation<TFail, TSuccess> operator >
            (Continuation<TFail, TSuccess> first,
                Func<TSuccess, TSuccess> thenMethod)
            => first.Then(thenMethod);

        /// <summary>
        /// Always raises a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="first">The continuation itself.</param>
        /// <param name="thenMethod">The function to apply when it is in <see cref="IsSuccess"/> state.</param>
        /// <returns>
        /// Always raises a <see cref="NotSupportedException"/>.
        /// </returns>
        public static Continuation<TFail, TSuccess> operator <
            (Continuation<TFail, TSuccess> first,
                Func<TSuccess, TSuccess> thenMethod)
            => throw new NotSupportedException();

        /// <summary>
        /// This allows a sophisticated and powerful way to apply some method in order to compose an operation with different functions.
        /// When the current <see cref="Continuation{TFail, TSuccess}"/> is <see cref="IsFail"/> the <paramref name="catchMethod"/> is applied.
        /// Otherwise, returns itself until encounter a <see cref="Then"/> function.
        /// </summary>
        /// <param name="first">The continuation itself.</param>
        /// <param name="catchMethod">The function to apply when it is in <see cref="IsFail"/> state.</param>
        /// <returns>
        /// Returns a new <see cref="Continuation{TFail, TSuccess}"/> value when the current value <see cref="IsFail"/>.
        /// Otherwise, returns itself.
        /// </returns>
        public static Continuation<TFail, TSuccess> operator >=
            (Continuation<TFail, TSuccess> first,
                Func<TFail, Continuation<TFail, TSuccess>> catchMethod)
            => first.Catch(catchMethod);

        /// <summary>
        /// Always raises a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="first">The continuation itself.</param>
        /// <param name="catchMethod">The function to apply when it is in <see cref="IsFail"/> state.</param>
        /// <returns>
        /// Always raises a <see cref="NotSupportedException"/>.
        /// </returns>
        public static Continuation<TFail, TSuccess> operator <=
            (Continuation<TFail, TSuccess> first, Func<TFail,
                Continuation<TFail, TSuccess>> catchMethod)
            => throw new NotSupportedException();
    }
}
