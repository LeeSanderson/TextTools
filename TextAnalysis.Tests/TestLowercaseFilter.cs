// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestLowercaseFilter.cs" company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
// <summary>
//   Tests for the LowercaseFilter class
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextAnalysis.Tests
{
    using System;
    using System.Linq;

    using Xunit;

    /// <summary>
    /// Tests for the <see cref="LowercaseFilter"/> class
    /// </summary>
    public class TestLowercaseFilter
    {
        /// <summary>
        /// When filter passed uppercase words then lowercase words are returned.
        /// </summary>
        [Fact]
        public void WhenFilterPassedUppercaseWordsThenLowercaseWordsAreReturned()
        {
            // Given
            var filter = new LowercaseFilter();

            // When
            var tokens = filter.Filter(new[] { "I", "LOVE", "Cats", "aNd", "dogS" });

            // Then
            Assert.Equal(new[] { "i", "love", "cats", "and", "dogs" }, tokens);
        }

        /// <summary>
        /// When filter passed null list then argument null exception is thrown.
        /// </summary>
        [Fact]
        public void WhenFilterPassedNullListThenArgumentNullExceptionIsThrown()
        {
            // Given
            var filter = new LowercaseFilter();

            // Then
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => filter.Filter(null).ToArray());
        }
    }
}
