// Copyright 2020 Heath Stewart
// Licensed under the MIT license. See LICENSE.txt in the project root for license information.

using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Threading.Tasks;
using Depends.Properties;

namespace Depends
{
    /// <summary>
    /// Main program.
    /// </summary>
    internal static class Program
    {
        private static async Task<int> Main(string[] args)
        {
            var verboseOption = new Option<bool>(new[] { "--verbose", "-v" }, Resources.VerboseOption);

            var analyze = new Command("analyze", Resources.AnalyzeDescription)
            {
                new Argument<FileInfo[]>("path")
                {
                    Arity = ArgumentArity.OneOrMore,
                    Description = Resources.AssemblyPathOption,
                }.ExistingOnly(),
                new Option<FileInfo>(new[] { "--output", "-o" }, Resources.OutputOption).LegalFilePathsOnly(),
                verboseOption,
            };
            analyze.Handler = CommandHandler.Create<FileInfo[], FileInfo, bool>(Analyze);

            var root = new RootCommand(Resources.RootDescription)
            {
                analyze,
            };

            // TODO: Use sync if no async down the stack is needed.
            return await root.InvokeAsync(args);
        }

        private static int Analyze(FileInfo[] path, FileInfo? output, bool verbose)
        {
            Console.WriteLine($"{nameof(path)} = {path.Join(", ", p => p.FullName)}");
            Console.WriteLine($"{nameof(output)} = {output?.FullName}");
            Console.WriteLine($"{nameof(verbose)} = {verbose}");

            return 0;
        }
    }
}
