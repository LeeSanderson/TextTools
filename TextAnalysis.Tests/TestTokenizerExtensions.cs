// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestTokenizerExtensions.cs" company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
// <summary>
//   Unit tests for <see cref="TokenizerExtensions" />
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextAnalysis.Tests
{
    using System;
    using System.Linq;

    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="TokenizerExtensions"/>
    /// </summary>
    public class TestTokenizerExtensions
    {
        /// <summary>
        /// When tokenize null string then argument null exception is thrown.
        /// </summary>
        [Fact]
        public void WhenTokenizeNullStringThenArgumentNullExceptionIsThrown()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => TokenizerExtensions.Tokenize(new BasicTokenizer(), null).ToArray());
        }

        /// <summary>
        /// When tokenize passed null tokenizer then argument null exception is thrown.
        /// </summary>
        [Fact]
        public void WhenTokenizePassedNullTokenizerThenArgumentNullExceptionIsThrown()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => TokenizerExtensions.Tokenize(null, "hello world").ToArray());
        }
    }
}
