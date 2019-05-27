// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NGram.cs" company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
// <summary>
//   Defines the NGram type - a collection of 1 or more tokens
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextAnalysis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using JetBrains.Annotations;

    /// <summary>
    /// Defines the NGram type - a collection of 1 or more tokens
    /// </summary>
    /// <typeparam name="T">The type of each token</typeparam>
    public class NGram<T> : IEquatable<NGram<T>>
    {
        /// <summary>
        /// Gets the grams.
        /// </summary>
        private readonly T[] grams;

        /// <summary>
        /// Initializes a new instance of the <see cref="NGram{T}"/> class.
        /// </summary>
        /// <param name="grams">The grams.</param>
        public NGram([NotNull] params T[] grams)
        {
            if (grams == null)
            {
                throw new ArgumentNullException(nameof(grams));
            }

            if (grams.Length == 0)
            {
                throw new ArgumentException(nameof(grams));
            }

            this.grams = grams;
        }

        /// <summary>
        /// Gets the number of grams
        /// </summary>
        public int Count => this.grams.Length;
        
        /// <summary>
        /// Gets the gram at the given index
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>The gram</returns>
        public T this[int index] => this.grams[index];

        /// <summary>
        /// Create a new instance of the <see cref="NGram{T}"/> class.
        /// </summary>
        /// <param name="grams">The grams.</param>
        /// <param name="startIndex">The start Index of the n-gram in the grams list.</param>
        /// <param name="count">The count of grams to copy from the grams list to create this n-gram. 
        /// If less than zero then all grams are copied</param>
        /// <returns>The new n-gram</returns>
        public static NGram<T> Create([NotNull] IList<T> grams, int startIndex = 0, int count = -1)
        {
            if (grams == null)
            {
                throw new ArgumentNullException(nameof(grams));
            }

            if (grams.Count == 0)
            {
                throw new ArgumentException(nameof(grams));
            }

            if (count <= 0)
            {
                count = grams.Count;
            }

            // Start index + count must be less than array length or the operation is undefined
            if (startIndex + count > grams.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }

            // Take defensive copy of buffer
            var g = new T[count];
            for (int i = 0; i < count; i++)
            {
                g[i] = grams[startIndex + i];
            }

            return new NGram<T>(g);
        }

        /// <summary>
        /// Overloaded == operator for comparing two n-grams.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>True if both n-grams are equal</returns>
        public static bool operator ==(NGram<T> left, NGram<T> right)
        {
            // ReSharper disable once RedundantNameQualifier
            return object.Equals(left, right);
        }

        /// <summary>
        /// Overloaded != operator for comparing two n-grams for inequality.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>False if both n-grams are equal</returns>
        public static bool operator !=(NGram<T> left, NGram<T> right)
        {
            // ReSharper disable once RedundantNameQualifier
            return !object.Equals(left, right);
        }

        /// <inheritdoc />
        public bool Equals(NGram<T> other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            // Check we have the same number of grams...
            if (this.grams.Length != other.grams.Length)
            {
                return false;
            }

            // ... and that they are in the same order
            var comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < this.grams.Length; i++)
            {
                if (!comparer.Equals(this.grams[i], other.grams[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((NGram<T>)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            const int CombiningPrime = 31;
            var factory = EqualityComparer<T>.Default;
            unchecked
            {
                int hash = 0;
                foreach (var gram in this.grams)
                {
                    hash = (CombiningPrime * hash) + factory.GetHashCode(gram);
                }

                return hash;
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Join(" ", this.grams.Select(g => g.ToString()));
        }
    }
}