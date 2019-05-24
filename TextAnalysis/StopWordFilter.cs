// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StopWordFilter.cs" company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
//   
// <summary>
//   A filter that excludes words that are in a stopword list
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextAnalysis
{
    using System;
    using System.Collections.Generic;

    using JetBrains.Annotations;

    /// <summary>
    /// A filter that excludes words that are in a stop word list
    /// </summary>
    public class StopWordFilter : IFilter<string, string>
    {
        /// <summary>
        /// The stop words.
        /// </summary>
        private readonly HashSet<string> stopwords;

        /// <summary>
        /// Initializes a new instance of the <see cref="StopWordFilter"/> class.
        /// </summary>
        /// <param name="stopwords">The stop words.</param>
        /// <param name="ignoreCase">Should we ignore the case of the word when filtering stop words or not?</param>
        public StopWordFilter([NotNull] IEnumerable<string> stopwords, bool ignoreCase = false)
        {
            if (stopwords == null)
            {
                throw new ArgumentNullException(nameof(stopwords));
            }

            this.stopwords = new HashSet<string>(stopwords, ignoreCase ? StringComparer.OrdinalIgnoreCase : null);
        }

        /// <inheritdoc />
        public IEnumerable<string> Filter(IEnumerable<string> input)
        {
            foreach (var s in input)
            {
                if (!this.stopwords.Contains(s))
                {
                    yield return s;
                }
            }
        }
    }
}