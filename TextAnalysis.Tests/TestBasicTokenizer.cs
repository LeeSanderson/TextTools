// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestBasicTokenizer.cs" company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
// <summary>
//   Test the <see cref="BasicTokenizer" />
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextAnalysis.Tests
{
    using System;
    using System.Linq;

    using Xunit;

    /// <summary>
    /// Test the <see cref="BasicTokenizer"/>
    /// </summary>
    public class TestBasicTokenizer
    {
        /// <summary>
        /// When tokenize string then words separated by spaces are returned as tokens.
        /// </summary>
        [Fact]
        public void WhenTokenizeStringThenWordsSeparatedBySpacesAreReturnedAsTokens()
        {
            // Given
            var tokenizer = new BasicTokenizer();

            // When
            var tokens = tokenizer.Tokenize("The cat sat on the mat");

            // Then
            Assert.Equal(new[] { "The", "cat", "sat", "on", "the", "mat" }, tokens);
        }

        /// <summary>
        /// The when tokenize string then words separated by punctuation are returned as tokens.
        /// </summary>
        [Fact]
        public void WhenTokenizeStringThenWordsSeparatedByPunctuationAreReturnedAsTokens()
        {
            // Given
            var tokenizer = new BasicTokenizer();

            // When
            var tokens = tokenizer.Tokenize("The,cat.sat/on-the(mat.");

            // Then
            Assert.Equal(new[] { "The", "cat", "sat", "on", "the", "mat" }, tokens);
        }

        /// <summary>
        /// When buffer size too small then argument out of range exception is thrown.
        /// </summary>
        [Fact]
        public void WhenBufferSizeTooSmallThenArgumentOutOfRangeExceptionIsThrown()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new BasicTokenizer(0));
        }

        /// <summary>
        /// When tokenize passed null text reader then argument null exception is thrown.
        /// </summary>
        [Fact]
        public void WhenTokenizePassedNullTextReaderThenArgumentNullExceptionIsThrown()
        {
            // Given
            var tokenizer = new BasicTokenizer();

            // Then
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => tokenizer.Tokenize(null).ToArray());
        }
    }
}
