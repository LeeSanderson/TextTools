// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestStopWordFilter.cs" company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
// <summary>
//   Unit tests for <see cref="StopWordFilter" />
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextAnalysis.Tests
{
    using System;

    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="StopWordFilter"/>
    /// </summary>
    public class TestStopWordFilter
    {
        /// <summary>
        /// When case sensitive filter created then expected stop words are removed.
        /// </summary>
        [Fact]
        public void WhenCaseSensitiveFilterCreatedThenExpectedStopWordsAreRemoved()
        {
            // Given
            var filter = new StopWordFilter(new[] { "cat" });

            // When
            var tokens = filter.Filter(new[] { "Cat", "cat", "dog" });

            // Then
            Assert.Equal(new[] { "Cat", "dog" }, tokens);
        }

        /// <summary>
        /// When case insensitive filter created then expected stop words are removed.
        /// </summary>
        [Fact]
        public void WhenCaseInsensitiveFilterCreatedThenExpectedStopWordsAreRemoved()
        {
            // Given
            var filter = new StopWordFilter(new[] { "cat" }, true);

            // When
            var tokens = filter.Filter(new[] { "Cat", "cat", "dog" });

            // Then
            Assert.Equal(new[] { "dog" }, tokens);
        }

        /// <summary>
        /// When filter created with null stop word list then argument null exception is thrown.
        /// </summary>
        [Fact]
        public void WhenFilterCreatedWithNullStopWordListThenArgumentNullExceptionIsThrown()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => new StopWordFilter(null));
        }
    }
}
