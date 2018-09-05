using System;
using System.Collections.Generic;
using NameSorter.Model;
using NameSorter.Model.Implementation;

namespace NameSorter.App
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName  = args[0];

            using (var sorter = new SimpleNameSorter {
                Source = new FileNameSource(fileName),
                Algorithm = new LinqSortAlgorithm(),
                Destinations = new List<INameDestination> {
                    new ConsoleNameDestination(),
                    new FileNameDestination(@"./sorted-names-list.txt")
                }})
            {
                sorter.Sort();
            }
            
        }
    }
}
