// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterExtensions.cs" company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
// <summary>
//   Defines the extension methods for the IFilter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextAnalysis
{
    /// <summary>
    /// Defines the extension methods for the <see cref="IFilter{TIn,TOut}"/> type.
    /// </summary>
    public static class FilterExtensions
    {
        /// <summary>
        /// Chain two filters together to create a new filter that is the chain of the source and sink
        /// </summary>
        /// <param name="source">The source filter.</param>
        /// <param name="sink">The sink filter.</param>
        /// <typeparam name="TIn">The type of the input to the source filter</typeparam>
        /// <typeparam name="TInOut">The type of input to the sink filter and output from the source filter</typeparam>
        /// <typeparam name="TOut">The type of output from the source filter</typeparam>
        /// <returns>A new filter that chains the source and sink together</returns>
        public static IFilter<TIn, TOut> Chain<TIn, TInOut, TOut>(
            this IFilter<TIn, TInOut> source,
            IFilter<TInOut, TOut> sink)
        {
            return new FilterChain<TIn, TInOut, TOut>(source, sink);
        }
    }
}
