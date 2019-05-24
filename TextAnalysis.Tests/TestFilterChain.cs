// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestFilterChain.cs" company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
// <summary>
//   Unit tests for the FilterChain class
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextAnalysis.Tests
{
    using System;

    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="FilterChain{TIn, TInOut, TOut}"/> class
    /// </summary>
    public class TestFilterChain
    {
        /// <summary>
        /// When use filter chain then expected tokens are removed.
        /// </summary>
        [Fact]
        public void WhenUseFilterChainThenExpectedTokensAreRemoved()
        {
            // Given
            var filter = new LowercaseFilter().Chain(new MinLengthFilter(2));

            // When
            var tokens = filter.Filter(new[] { "A", "AA", "AAB" });

            // Then
            Assert.Equal(new[] { "aa", "aab" }, tokens);
        }

        /// <summary>
        /// When create filter chain with null source then argument null exception is thrown.
        /// </summary>
        [Fact]
        public void WhenCreateFilterChainWithNullSourceThenArgumentNullExceptionIsThrown()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(
                () => new FilterChain<string, string, string>(null, new LowercaseFilter()));
        }

        /// <summary>
        /// When create filter chain with null sink then argument null exception is thrown.
        /// </summary>
        [Fact]
        public void WhenCreateFilterChainWithNullSinkThenArgumentNullExceptionIsThrown()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(
                () => new FilterChain<string, string, string>(new LowercaseFilter(), null));
        }
    }
}
