// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Options.cs" company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
// <summary>
//   Base class for options for a command.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextTool.Console
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using CommandLine;

    /// <summary>
    /// Base class for options for a command.
    /// </summary>
    internal abstract class Options
    {
        /// <summary>
        /// Gets or sets a value indicating whether the command should output verbose messages
        /// </summary>
        [Option('v', "verbose", Default = false, HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }

        /// <summary>
        /// Validate the options to ensure they are logically valid (e.g. file properties reference files that exist).
        /// </summary>
        /// <returns>The list of errors (if any)</returns>
        internal abstract IEnumerable<string> Validate();
    }
}