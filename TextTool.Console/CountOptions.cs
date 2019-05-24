// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CountOptions.cs"  company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
// <summary>
//   Options for the "count" command.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextTool.Console
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    using CommandLine;

    /// <summary>
    /// Options for the "count" command.
    /// </summary>
    [Verb("count", HelpText = "Count the number of unique words in a given text file")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global", Justification = "Created by the command line parser")]
    internal class CountOptions : Options
    {
        /// <summary>
        /// Gets or sets the input text file containing the words to be counted
        /// </summary>
        [Option('i', "input", Required = true, HelpText = "The input text file to be processed.")]
        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global", Justification = "Set by command liner parser")]
        public string InputFile { get; set; }

        /// <summary>
        /// Gets or sets the input text file containing the words to be counted
        /// </summary>
        [Option('s', "stopwords", Required = false, HelpText = "The text file containing the stop words.")]
        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global", Justification = "Set by command liner parser")]
        public string StopWordFile { get; set; }

        /// <summary>
        /// Gets or sets the minimum length of tokens to be included in the count
        /// </summary>
        [Option('m', "minlen", Required = false, HelpText = "The minimum length for a tokens to be included in the count.")]
        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global", Justification = "Set by command liner parser")]
        public int? MinLength { get; set; }

        /// <inheritdoc />
        internal override IEnumerable<string> Validate()
        {
            if (!File.Exists(this.InputFile))
            {
                yield return $"Input file '{this.InputFile}' does not exist";
            }

            if (!string.IsNullOrEmpty(this.StopWordFile) && !File.Exists(this.StopWordFile))
            {
                yield return $"Stop word file '{this.StopWordFile}' does not exist";
            }

            if (this.MinLength != null && this.MinLength.Value < 1)
            {
                yield return "MinLength must be 1 or more.";
            }
        }
    }
}