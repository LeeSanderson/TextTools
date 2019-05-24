// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Command.cs" company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
// <summary>
//   Base class for commands that execute a program command
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextTool.Console
{
    using System;
    using System.IO;
    using System.Linq;

    using JetBrains.Annotations;

    /// <summary>
    /// Base class for commands that execute a program command
    /// </summary>
    /// <typeparam name="TOptions">The type of the options that the command requires</typeparam>
    internal abstract class Command<TOptions>
        where TOptions : Options
    {
        /// <summary>
        /// The output that the command may write to.
        /// </summary>
        private readonly TextWriter output;

        /// <summary>
        /// Initializes a new instance of the <see cref="Command{TOptions}"/> class.
        /// </summary>
        /// <param name="output">The output that the command may write to</param>
        /// <param name="options">The options.</param>
        protected Command(TextWriter output, TOptions options)
        {
            this.Options = options;
            this.output = output;
        }

        /// <summary>
        /// Gets the options.
        /// </summary>
        protected TOptions Options { get; }

        /// <summary>
        /// Run the command - delegates to <see cref="InternalRun"/>
        /// </summary>
        /// <returns>The program exit code</returns>
        public int Run()
        {
            try
            {
                // Ensure command parameters are valid
                var commandErrors = this.Options.Validate().ToList();
                if (commandErrors.Any())
                {
                    this.output.WriteLine("ERROR:");
                    foreach (var commandError in commandErrors)
                    {
                        this.output.WriteLine($"\t{commandError}");
                    }

                    return 1;
                }

                // Parameters valid - execute command
                var start = Environment.TickCount;               
                var exitCode = this.InternalRun();                
                this.WriteOut("Command completed in {0} ms.", Environment.TickCount - start);
                return exitCode;
            }
            catch (Exception e)
            {
                this.output.WriteLine($"Program terminated with unexpected exception '{e.Message}'");
                return 1;
            }
        }

        /// <summary>
        /// Run the actual command (safe in the knowledge that exceptions will be handled and reported)
        /// </summary>
        /// <returns>The program exit code</returns>
        protected abstract int InternalRun();

        /// <summary>
        /// Write a formatted string to the output
        /// </summary>
        /// <param name="s">The sting to write</param>
        /// <param name="args">The output arguments</param>
        [StringFormatMethod("s")]
        protected void WriteOut(string s, params object[] args)
        {
            this.output.WriteLine(s, args);
        }
    }
}