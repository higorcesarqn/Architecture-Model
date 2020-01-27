﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tango.Types;

namespace Tango.Test.Types
{
    [TestClass]
    public class EitherTests
    {
        [TestMethod]
        public void EitherMatch2WhenBothLeft()
        {
            int expected = 25;
            Either<bool, int> either = 15;
            Either<bool, int> either2 = 10;
            int result =
                either.Match2(
                either2,
                (value1, value2) => value1 + value2,
                (value1, value2) => value1,
                (value1, value2) => value2,
                (value1, value2) => 0);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherMatch2WhenLeftRight()
        {
            int expected = 15;
            Either<bool, int> either = 15;
            Either<bool, int> either2 = true;
            int result =
                either.Match2(
                either2,
                (value1, value2) => value1 + value2,
                (value1, value2) => value1,
                (value1, value2) => value2,
                (value1, value2) => 0);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherMatch2WhenRightLeft()
        {
            int expected = 10;
            Either<bool, int> either = true;
            Either<bool, int> either2 = 10;
            int result =
                either.Match2(
                either2,
                (value1, value2) => value1 + value2,
                (value1, value2) => value1,
                (value1, value2) => value2,
                (value1, value2) => 0);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherMatch2WhenRightRight()
        {
            int expected = 0;
            Either<bool, int> either = true;
            Either<bool, int> either2 = true;
            int result =
                either.Match2(
                either2,
                (value1, value2) => value1 + value2,
                (value1, value2) => value1,
                (value1, value2) => value2,
                (value1, value2) => 0);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherMatch2WithActionWhenBothLeft()
        {
            int expected = 35;
            int result = 10;
            Either<bool, int> either = 15;
            Either<bool, int> either2 = 10;
            either.Match2(
                either2,
                (value1, value2) => { result += value1 + value2; },
                (value1, value2) => { result += value1; },
                (value1, value2) => { result += value2; },
                (value1, value2) => { result += 0; });

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherMatch2WithActionWhenLeftRight()
        {
            int expected = 25;
            int result = 10;
            Either<bool, int> either = 15;
            Either<bool, int> either2 = true;
            either.Match2(
                either2,
                (value1, value2) => { result += value1 + value2; },
                (value1, value2) => { result += value1; },
                (value1, value2) => { result += value2; },
                (value1, value2) => { result += 0; });

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherMatch2WithActionWhenRightLeft()
        {
            int expected = 20;
            int result = 10;
            Either<bool, int> either = true;
            Either<bool, int> either2 = 10;
            either.Match2(
                either2,
                (value1, value2) => { result += value1 + value2; },
                (value1, value2) => { result += value1; },
                (value1, value2) => { result += value2; },
                (value1, value2) => { result += 0; });

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherMatch2WhenBothRightRight()
        {
            int expected = 13;
            int result = 10;
            Either<bool, int> either = true;
            Either<bool, int> either2 = true;
            either.Match2(
                either2,
                (value1, value2) => { result += value1 + value2; },
                (value1, value2) => { result += value1; },
                (value1, value2) => { result += value2; },
                (value1, value2) => { result += 3; });

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherFromContinuationRight()
        {
            int expected = 10;
            Continuation<string, int> continuation = 10;
            Either<string, int> either = continuation;
            int result = either.Match(success => success, fail => 0);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EitherFromContinuationLeft()
        {
            int expected = 0;
            Continuation<string, int> continuation = "Hello World";
            Either<string, int> either = continuation;
            int result = either.Match(success => success, fail => 0);

            Assert.AreEqual(expected, result);
        }

    }
}
