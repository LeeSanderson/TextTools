// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Lee Sanderson">
//   Copyright (c) Lee Sanderson.
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextTool.Console
{
    using System;

    using CommandLine;

    /// <summary>
    /// The program.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main program entry point.
        /// </summary>
        /// <param name="args">The command line args.</param>
        /// <returns>The exit code of the program</returns>
        public static int Main(string[] args)
        {
            var output = Console.Out;
            try
            {
                return Parser.Default.ParseArguments<CountOptions>(args).MapResult(
                    opts => new CountCommand(output, opts).Run(),
                    err => 1);
            }
            catch (Exception e)
            {
                output.WriteLine($"Program terminated with unexpected exception '{e.Message}'");
                return 1;
            }
        }        
    }
}
