// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITokenizer.cs" company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
// <summary>
//   Define contract for tokenization of raw text into tokens
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextAnalysis
{
    using System.Collections.Generic;
    using System.IO;

    using JetBrains.Annotations;

    /// <summary>
    /// Define contract for tokenization of raw text into tokens
    /// </summary>
    public interface ITokenizer
    {
        /// <summary>
        /// Tokenize the text from the reader
        /// </summary>
        /// <param name="reader">A text reader representing the source text to be tokenized</param>
        /// <returns>An enumeration of token strings</returns>
        IEnumerable<string> Tokenize([NotNull] TextReader reader);        
    }
}
