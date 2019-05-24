// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFilter.cs"  company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
// <summary>
//   Define contract for objects that filter/convert/transform an input enumeration to an output enumeration
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextAnalysis
{
    using System.Collections.Generic;

    /// <summary>
    /// Define contract for objects that filter/convert/transform an input enumeration to an output enumeration
    /// </summary>
    /// <typeparam name="TIn">The type of the input enumeration values</typeparam>
    /// <typeparam name="TOut">The type of the output enumeration values</typeparam>
    public interface IFilter<in TIn, out TOut>
    {
        /// <summary>
        /// Filter the input
        /// </summary>
        /// <param name="input">The input</param>
        /// <returns>A filtered/converted/transformed output</returns>
        IEnumerable<TOut> Filter(IEnumerable<TIn> input);
    }
}
