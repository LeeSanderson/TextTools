// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestMinLengthFilter.cs" company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
// <summary>
//   Unit tests for the MinLengthFilter class
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextAnalysis.Tests
{
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="MinLengthFilter"/> class
    /// </summary>
    public class TestMinLengthFilter
    {
        /// <summary>
        /// When filter applied then expected tokens are removed.
        /// </summary>
        [Fact]
        public void WhenFilterAppliedThenExpectedTokensAreRemoved()
        {
            // Given
            var filter = new MinLengthFilter(4);

            // When
            var tokens = filter.Filter(new[] { "one", "two", "three", "four" });

            // Then
            Assert.Equal(new[] { "three", "four"}, tokens);
        }
    }
}
