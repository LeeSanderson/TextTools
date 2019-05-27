// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestNGramFilter.cs" company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
// <summary>
//   Tests for the NGramFilter
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextAnalysis.Tests
{
    using System;
    using System.Linq;

    using Xunit;

    /// <summary>
    /// Tests for the <see cref="NGramFilter{T}"/>
    /// </summary>
    public class TestNGramFilter
    {
        /// <summary>
        /// when tokenize a string then expected tokens are returned.
        /// </summary>
        [Fact]
        public void WhenTokenizeStringThenExpectedTokensAreReturned()
        {
            // Given
            var tokenizer = new BasicTokenizer();
            var filter = new NGramFilter<string>(new[] { 1, 3 });
            var results = filter.Filter(tokenizer.Tokenize("cat sat mat")).ToList();

            // Then
            Assert.Equal(new[] { N("cat"), N("sat"), N("mat"), N("cat", "sat", "mat") }, results);
        }

        /// <summary>
        /// when tokenize a string then expected tokens are returned.
        /// </summary>
        [Fact]
        public void WhenTokenizeStringWith1And2And3NGramsThenExpectedTokensAreReturned()
        {
            // Given
            var tokenizer = new BasicTokenizer();
            var filter = new NGramFilter<string>(new[] { 1, 2, 3 });
            var results = filter.Filter(tokenizer.Tokenize("cat sat mat")).ToList();

            // Then
            Assert.Equal(new[] { N("cat"), N("sat"), N("cat", "sat"), N("mat"), N("sat", "mat"), N("cat", "sat", "mat") }, results);
        }

        /// <summary>
        /// The when create filter with null n gram list then argument null exception is thrown.
        /// </summary>
        [Fact]
        public void WhenCreateFilterWithNullNGramSizesListThenArgumentNullExceptionIsThrown()
        {
            // Then
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => new NGramFilter<string>(null));
        }

        /// <summary>
        /// The when create filter with empty n gram sizes list then argument null exception is thrown.
        /// </summary>
        [Fact]
        public void WhenCreateFilterWithEmptyNGramSizesListThenArgumentNullExceptionIsThrown()
        {
            // Then
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => new NGramFilter<string>(new int[0]));
        }

        /// <summary>
        /// Factory method to generate n-grams
        /// </summary>
        /// <param name="tokens">The tokens</param>
        /// <returns>An ng-ram</returns>
        private static NGram<string> N(params string[] tokens)
        {
            return new NGram<string>(tokens);
        }
    }
}
