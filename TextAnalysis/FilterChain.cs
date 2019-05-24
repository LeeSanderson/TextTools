// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterChain.cs"  company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
// <summary>
//   A filter adapter to chain multiple filters together
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextAnalysis
{
    using System;
    using System.Collections.Generic;

    using JetBrains.Annotations;

    /// <summary>
    /// A filter adapter to chain multiple filters together
    /// </summary>
    /// <typeparam name="TIn">The input type</typeparam>
    /// <typeparam name="TInOut">The output type of the source filter and the input type of the sink filter</typeparam>
    /// <typeparam name="TOut">The output type</typeparam>
    public class FilterChain<TIn, TInOut, TOut> : IFilter<TIn, TOut>
    {
        /// <summary>
        /// The source.
        /// </summary>
        private readonly IFilter<TIn, TInOut> source;

        /// <summary>
        /// The sink.
        /// </summary>
        private readonly IFilter<TInOut, TOut> sink;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterChain{TIn,TInOut,TOut}"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="sink">The sink.</param>
        public FilterChain([NotNull] IFilter<TIn, TInOut> source, [NotNull] IFilter<TInOut, TOut> sink)
        {
            this.source = source ?? throw new ArgumentNullException(nameof(source));
            this.sink = sink ?? throw new ArgumentNullException(nameof(sink));
        }

        /// <inheritdoc />
        public IEnumerable<TOut> Filter(IEnumerable<TIn> input)
        {
            return this.sink.Filter(this.source.Filter(input));
        }
    }
}
