// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WordCounter.cs" company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
// <summary>
//   A class to count the unique words in a word list
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextAnalysis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using JetBrains.Annotations;

    /// <summary>
    /// A class to count the unique words in a word list
    /// </summary>
    /// <typeparam name="TWord">The type of the words to be counted</typeparam>
    public class WordCounter<TWord>
    {
        /// <summary>
        /// Count each of the unique
        /// </summary>
        /// <param name="words">The list of words to be counted</param>
        /// <returns>The dictionary of unique words</returns>
        public Dictionary<TWord, int> Count([NotNull] IEnumerable<TWord> words)
        {
            if (words == null)
            {
                throw new ArgumentNullException(nameof(words));
            }

            return words.GroupBy(word => word).ToDictionary(group => group.Key, group => group.Count());
        }

        /// <summary>
        /// Get the top <see cref="maxWords"/> count words by count
        /// </summary>
        /// <param name="words">The list of words to be counted</param>
        /// <param name="maxWords">The maximum number of words to return in the list</param>
        /// <returns>The list of words</returns>
        public List<WordCount<TWord>> TopCount(IEnumerable<TWord> words, int maxWords)
        {
            if (maxWords <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxWords), "maxWords must be greater that 0");
            }

            return this.Count(words).OrderByDescending(kv => kv.Value).Take(maxWords)
                .Select(kv => new WordCount<TWord>(kv.Key, kv.Value))
                .ToList();
        }
    }
}
