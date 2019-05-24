// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MinLengthFilter.cs" company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
// <summary>
//   A filter that removes tokens that are too short.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextAnalysis
{
    using System.Collections.Generic;

    /// <summary>
    /// A filter that removes tokens that are too short.
    /// </summary>
    public class MinLengthFilter : IFilter<string, string>
    {
        /// <summary>
        /// The min length of tokens that the filter allows.
        /// </summary>
        private readonly int minLength;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinLengthFilter"/> class.
        /// </summary>
        /// <param name="minLength">The minimum length of token allowed by the filter.</param>
        public MinLengthFilter(int minLength)
        {
            this.minLength = minLength;
        }

        /// <inheritdoc />
        public IEnumerable<string> Filter(IEnumerable<string> input)
        {
            foreach (var token in input)
            {
                if (token.Length >= this.minLength)
                {
                    yield return token;
                }
            }
        }
    }
}
