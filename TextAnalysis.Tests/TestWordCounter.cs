// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestWordCounter.cs" company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
// <summary>
//   Tests for the <see cref="WordCounter" /> class
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextAnalysis.Tests
{
    using System;

    using Xunit;

    /// <summary>
    /// Tests for the <see cref="WordCounter{TWord}"/> class
    /// </summary>
    public class TestWordCounter
    {
        /// <summary>
        /// When count passed list of words then expected values are returned.
        /// </summary>
        [Fact]
        public void WhenCountPassedListOfWordsThenExpectedValuesAreReturned()
        {
            // Given
            var counter = new WordCounter<string>();

            // When
            var wordCounts = counter.Count(new[] { "1", "2", "3", "2", "3", "3" });

            // Then
            Assert.Equal(1, wordCounts["1"]);
            Assert.Equal(2, wordCounts["2"]);
            Assert.Equal(3, wordCounts["3"]);
            Assert.Equal(3, wordCounts.Count);
        }

        /// <summary>
        /// When top count passed list of words then expected values are returned.
        /// </summary>
        [Fact]
        public void WhenTopCountPassedListOfWordsThenExpectedValuesAreReturned()
        {
            // Given
            var counter = new WordCounter<string>();

            // When
            var wordCounts = counter.TopCount(new[] { "1", "2", "3", "2", "3", "3" }, 2);

            // Then
            Assert.Equal(2, wordCounts.Count);
            Assert.Equal(3, wordCounts[0].Count);
            Assert.Equal(2, wordCounts[1].Count);
            Assert.Equal("3", wordCounts[0].Word);
            Assert.Equal("2", wordCounts[1].Word);
        }

        /// <summary>
        /// When count passed null list then argument null exception is thrown.
        /// </summary>
        [Fact]
        public void WhenCountPassedNullListThenArgumentNullExceptionIsThrown()
        {
            // Given
            var counter = new WordCounter<string>();

            // Then
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => counter.Count(null));
        }

        /// <summary>
        /// When top count passed zero max words then argument out of range exception is thrown.
        /// </summary>
        [Fact]
        public void WhenTopCountPassedZeroMaxWordsThenArgumentOutOfRangeExceptionIsThrown()
        {
            // Given
            var counter = new WordCounter<string>();

            // Then
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentOutOfRangeException>(() => counter.TopCount(new[] { "words" }, 0));
        }
    }
}
