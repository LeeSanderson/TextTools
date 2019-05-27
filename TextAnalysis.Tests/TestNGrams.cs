// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestNGrams.cs" company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
// <summary>
//   Test the <see cref="NGram{T}" /> class
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextAnalysis.Tests
{
    using System;
    using System.Collections.Generic;

    using Xunit;

    /// <summary>
    /// Test the <see cref="NGram{T}"/> class
    /// </summary>
    public class TestNGrams
    {
        /// <summary>
        /// When compare different typed n grams for equality the expected results are returned.
        /// </summary>
        [Fact]
        public void WhenCompareDifferentTypedNGramsForEqualityTheExpectedResultsAreReturned()
        {
            // Given
            var ng1 = new NGram<int>(1);
            var ng2 = new NGram<string>("1");

            // Then
            Assert.NotEqual(ng1.GetHashCode(), ng2.GetHashCode());

            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.False(ng1.Equals(ng2));

            Assert.Equal("1", ng1.ToString());
            Assert.Equal("1", ng2.ToString());
            Assert.Equal(1, ng1.Count);
            Assert.Equal(1, ng1[0]);
        }

        /// <summary>
        /// When compare n grams with different number of n grams for equality the expected results are returned.
        /// </summary>
        [Fact]
        public void WhenCompareNGramsWithDifferentNumberOfNGramsForEqualityTheExpectedResultsAreReturned()
        {
            // Given
            var ng1 = new NGram<string>("1");
            var ng2 = new NGram<string>("1", "2");

            // Then
            Assert.NotEqual(ng1.GetHashCode(), ng2.GetHashCode());
            Assert.False(ng1 == ng2);
            Assert.Equal("1", ng1.ToString());
            Assert.Equal("1 2", ng2.ToString());
        }

        /// <summary>
        /// When compare identical n grams for equality the expected results are returned.
        /// </summary>
        [Fact]
        public void WhenCompareIdenticalNGramsForEqualityTheExpectedResultsAreReturned()
        {
            // Given
            var ng1 = new NGram<string>("1", "2");
            var ng2 = new NGram<string>("1", "2");
            var ng3 = new NGram<string>("2", "1");

            // Then
            Assert.Equal(ng1.GetHashCode(), ng2.GetHashCode());
            Assert.NotEqual(ng1.GetHashCode(), ng3.GetHashCode());
            Assert.NotEqual(ng2.GetHashCode(), ng3.GetHashCode());

            Assert.True(ng1 == ng2);
            Assert.False(ng1 == ng3);
            Assert.False(ng2 == ng3);
            Assert.False(ng1 != ng2);
            Assert.True(ng1 != ng3);
            Assert.True(ng2 != ng3);
            Assert.False(ng1.Equals((object)null));
            Assert.False(ng1.Equals(null));
            Assert.True(ng1.Equals((object)ng1));
            Assert.True(ng1.Equals(ng1));

            Assert.Equal("1 2", ng1.ToString());
            Assert.Equal("1 2", ng2.ToString());
            Assert.Equal("2 1", ng3.ToString());
        }

        /// <summary>
        /// When create n gram and grams array is null then argument null exception is thrown.
        /// </summary>
        [Fact]
        public void WhenCreateNGramAndGramsArrayIsNullThenArgumentNullExceptionIsThrown()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => new NGram<int>(null));
        }

        /// <summary>
        /// When create n gram and grams array is empty then argument exception is thrown.
        /// </summary>
        [Fact]
        public void WhenCreateNGramAndGramsArrayIsEmptyThenArgumentExceptionIsThrown()
        {
            Assert.Throws<ArgumentException>(() => new NGram<int>());
        }

        /// <summary>
        /// When create n gram from list and list is null then argument null exception is thrown.
        /// </summary>
        [Fact]
        public void WhenCreateNGramFromListAndListIsNullThenArgumentNullExceptionIsThrown()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => NGram<int>.Create(null));
        }

        /// <summary>
        /// When create n gram from list and list is empty then argument null exception is thrown.
        /// </summary>
        [Fact]
        public void WhenCreateNGramFromListAndListIsEmptyThenArgumentExceptionIsThrown()
        {
            Assert.Throws<ArgumentException>(() => NGram<int>.Create(new List<int>()));
        }

        /// <summary>
        /// When create n gram from list and list offset is greater than size of list then argument out of range exception is thrown.
        /// </summary>
        [Fact]
        public void WhenCreateNGramFromListAndListOffsetIsGreaterThanSizeOfListThenArgumentOutOfRangeExceptionIsThrown()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => NGram<int>.Create(new List<int> { 1, 2 }, 3));
        }
    }
}
