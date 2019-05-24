// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CharacterTokenizer.cs" company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
// <summary>
//   An abstract tokenizer that tokenizes text at specific character boundaries
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextAnalysis
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// An abstract tokenizer that tokenizes text at specific character boundaries
    /// </summary>
    public abstract class CharacterTokenizer : ITokenizer
    {
        /// <summary>
        /// The size of the buffer.
        /// </summary>
        private readonly int bufferSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterTokenizer"/> class.
        /// </summary>
        /// <param name="bufferSize">
        /// The buffer size - must be greater than 0.
        /// </param>
        protected CharacterTokenizer(int bufferSize = 1024)
        {
            if (bufferSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bufferSize));
            }

            this.bufferSize = bufferSize;
        }

        /// <inheritdoc />
        public IEnumerable<string> Tokenize(TextReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            char[] buffer = new char[this.bufferSize];
            StringBuilder builder = new StringBuilder(buffer.Length);
            while (true)
            {
                var readCount = reader.Read(buffer, 0, buffer.Length);
                if (readCount == 0)
                {
                    // Found the end of the reader - no more characters to read
                    break;
                }

                // Else - process the buffer
                for (int i = 0; i < readCount; i++)
                {
                    if (this.IsTokenChar(buffer[i]))
                    {
                        // Append token character to buffer.
                        builder.Append(buffer[i]);
                    }
                    else
                    {
                        // Found a non-token character
                        if (builder.Length > 0)
                        {
                            // Output the builder and reset
                            yield return builder.ToString();
                            builder.Length = 0;
                        }
                    }
                }
            }

            // Check to see if we have any characters in the builder
            if (builder.Length > 0)
            {
                yield return builder.ToString();
            }
        }

        /// <summary>
        /// Checks whether a given character is a token character or not.
        /// Token characters are appended to an internal buffer and returned in the output of <see cref="Tokenize(TextReader)"/>.
        /// Non-token characters are skipped (and used as boundaries to tokens).
        /// </summary>
        /// <param name="c">The character to be checked</param>
        /// <returns>True if the character is a token character</returns>
        protected abstract bool IsTokenChar(char c);
    }
}