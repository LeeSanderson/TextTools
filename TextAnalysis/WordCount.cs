// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WordCount.cs" company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
// <summary>
//   A class to represent a word and the number of time it appears in a list of words
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextAnalysis
{
    /// <summary>
    /// A class to represent a word and the number of time it appears in a list of words
    /// </summary>
    /// <typeparam name="TWord">The type of the word</typeparam>
    public class WordCount<TWord>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WordCount{TWord}"/> class.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="count">The count.</param>
        public WordCount(TWord word, int count)
        {
            this.Word = word;
            this.Count = count;
        }

        /// <summary>
        /// Gets the word.
        /// </summary>
        public TWord Word { get; }

        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count { get; }
    }
}
