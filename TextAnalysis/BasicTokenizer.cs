// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasicTokenizer.cs" company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
// <summary>
//   A tokenizer that splits tokens on whitespace boundaries or punctuation marks
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextAnalysis
{
    /// <summary>
    /// A tokenizer that splits tokens on whitespace boundaries or punctuation marks 
    /// </summary>
    public class BasicTokenizer : CharacterTokenizer
    {
        /// <inheritdoc />
        public BasicTokenizer(int bufferSize = 1024)
            : base(bufferSize)
        {
        }

        /// <inheritdoc />
        protected override bool IsTokenChar(char c)
        {
            return !char.IsWhiteSpace(c) && !char.IsPunctuation(c);
        }
    }
}
