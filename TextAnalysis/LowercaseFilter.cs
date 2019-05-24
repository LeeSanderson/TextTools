// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LowercaseFilter.cs" company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
// <summary>
//   A filter that converts strings to lowercase
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextAnalysis
{
    using System;
    using System.Collections.Generic;

    using JetBrains.Annotations;

    /// <summary>
    /// A filter that converts strings to lowercase
    /// </summary>
    public class LowercaseFilter : IFilter<string, string>
    {
        /// <summary>
        /// Filter the input and convert string values to lower case
        /// </summary>
        /// <param name="input">The input values</param>
        /// <returns>An enumeration of lowercase values</returns>
        public IEnumerable<string> Filter([NotNull] IEnumerable<string> input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            foreach (var s in input)
            {
                yield return s.ToLower();
            }
        }
    }
}
