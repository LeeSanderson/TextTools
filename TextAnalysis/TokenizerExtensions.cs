// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TokenizerExtensions.cs" company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
// <summary>
//   Extension methods for the classes that implement the <see cref="ITokenizer" /> interface
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextAnalysis
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using JetBrains.Annotations;

    /// <summary>
    /// Extension methods for the classes that implement the <see cref="ITokenizer"/> interface
    /// </summary>
    public static class TokenizerExtensions
    {
        /// <summary>
        /// Utility method to tokenize a string by wrapping it in a string reader
        /// </summary>
        /// <param name="tokenizer">The tokenizer</param>
        /// <param name="text">The text to tokenize</param>
        /// <returns>An enumeration of tokens</returns>
        public static IEnumerable<string> Tokenize([NotNull] this ITokenizer tokenizer, [NotNull] string text)
        {
            if (tokenizer == null)
            {
                throw new ArgumentNullException(nameof(tokenizer));
            }

            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            using (var reader = new StringReader(text))
            {
                // Must make this the wrapper method an iterator to ensure StringReader is not disposed before enumeration.
                foreach (var token in tokenizer.Tokenize(reader))
                {
                    yield return token;
                }
            }
        }
    }
}
