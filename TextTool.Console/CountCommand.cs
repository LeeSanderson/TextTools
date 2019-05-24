// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CountCommand.cs" company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
// <summary>
//   A command to count the number of unique words in an input file and output the top 10 (with counts).
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextTool.Console
{
    using System.Collections.Generic;
    using System.IO;

    using TextAnalysis;

    /// <summary>
    /// A command to count the number of unique words in an input file and output the top 10 (with counts).
    /// </summary>
    internal class CountCommand : Command<CountOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountCommand"/> class.
        /// </summary>
        /// <param name="output">The output that the command may write to</param>
        /// <param name="options">The options.</param>
        public CountCommand(TextWriter output, CountOptions options) : base(output, options)
        {
        }

        /// <inheritdoc />
        protected override int InternalRun()
        {
            var tokenizer = new BasicTokenizer();
            IFilter<string, string> filter = new LowercaseFilter();
            if (!string.IsNullOrEmpty(this.Options.StopWordFile))
            {
                // Load stop words and set up filter chain
                filter = filter.Chain(new StopWordFilter(File.ReadAllLines(this.Options.StopWordFile)));
            }

            if (this.Options.MinLength != null)
            {
                filter = filter.Chain(new MinLengthFilter(this.Options.MinLength.Value));
            }

            var wordCounter = new WordCounter<string>();
            List<WordCount<string>> topWords;

            using (var inputFile = new FileStream(this.Options.InputFile, FileMode.Open, FileAccess.Read))
            {
                using (var textReader = new StreamReader(inputFile))
                {
                    topWords = wordCounter.TopCount(filter.Filter(tokenizer.Tokenize(textReader)), 10);
                }
            }

            // Sort topwords by frequency
            foreach (var topWord in topWords)
            {
                this.WriteOut("{0} - {1}", topWord.Word, topWord.Count);
            }

            return 0;
        }
    }
}