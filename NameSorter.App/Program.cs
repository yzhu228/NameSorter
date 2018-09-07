
namespace NameSorter.App
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using CommandLine;
    using NameSorter.Model;
    using NameSorter.Model.Implementation;

    class Options
    {
        [Value(0, MetaName="Input Filename", Required = true, HelpText = "Names file to be sorted.")]
        public string InputFilename { get; set; }
        [Option('o', "output", HelpText="Output file name")]
        public string OutputFilename { get; set; }
        [Option('d', "desc", Default=false, HelpText="Sort name list descendingly")]
        public bool IsDecending { get; set; }
    }

    class Program
    {
        static readonly string DefaultOutputFile = @"./sorted-names-list.txt";
        static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<Options>(args)
                .WithParsed(sortName);
            return;
        }

        private static void sortName(Options option) {
            var inFileName = option.InputFilename;
            var outFileName = !string.IsNullOrWhiteSpace(option.OutputFilename) ?
                option.OutputFilename : DefaultOutputFile;
            var algorithm = !option.IsDecending 
                    ? new LinqAscSortAlgorithm() as ISortAlgorithm 
                    : new LinqDescSortAlgorithm() as ISortAlgorithm;
            try {
                using (var sorter = new SimpleNameSorter {
                    Source = new FileNameSource(inFileName),
                    Algorithm = algorithm,
                    Destinations = new List<INameDestination> {
                        new ConsoleNameDestination(),
                        new FileNameDestination(outFileName)}
                    })
                {
                    sorter.Sort();
                }
            }
            catch (IOException ex) {
                handleError(ex, "File operation error!");
            }
            catch (Exception ex) {
                handleError(ex, "Unexpected error!");
            }
        }

        private static void handleError(Exception ex, string message = "") {
            Console.Error.WriteLine($"\nError: {message}");
            Console.Error.WriteLine(ex.Message);
            Console.Error.WriteLine();
        }
    }
}
