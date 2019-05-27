// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NGramFilter.cs" company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
// <summary>
//   A filter that generates n-grams from a source input
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextAnalysis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using JetBrains.Annotations;

    /// <summary>
    /// A filter that generates n-grams from a source input
    /// </summary>
    /// <typeparam name="T">The type of n-gram to generate</typeparam>
    public class NGramFilter<T> : IFilter<T, NGram<T>>
    {
        /// <summary>Define a circular buffer to store the grams in</summary>
        private readonly CircularBuffer<T> buffer;

        /// <summary>Define the sizes of grams we want</summary>
        private readonly int[] gramSizes;

        /// <inheritdoc />
        public NGramFilter([NotNull] IEnumerable<int> gramSizes)
        {
            if (gramSizes == null)
            {
                throw new ArgumentNullException(nameof(gramSizes));
            }

            // Take a defensive copy of the gram sizes we are to generate
            this.gramSizes = gramSizes.ToArray();
            if (this.gramSizes.Length == 0)
            {
                throw new ArgumentNullException(nameof(gramSizes));
            }

            // Sort the buffer so we always return the smallest grams first.
            Array.Sort(this.gramSizes);

            // Now the list is sorted we can easily find the max (which we need to create the buffer) by taking the last value
            var max = this.gramSizes[this.gramSizes.Length - 1];
            this.buffer = new CircularBuffer<T>(max);
        }

        /// <inheritdoc />
        public IEnumerable<NGram<T>> Filter(IEnumerable<T> input)
        {
            foreach (var token in input)
            {
                this.buffer.Add(token);
                for (int i = 0; i < this.gramSizes.Length; i++)
                {
                    // Make sure the buffer is big enough to generate a gram of the required size
                    var gramSize = this.gramSizes[i];
                    if (this.buffer.Count >= gramSize)
                    {
                        yield return NGram<T>.Create(this.buffer, this.buffer.Count - gramSize, gramSize);
                    }
                }
            }
        }
    }
}
