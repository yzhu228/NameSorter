namespace NameSorter
{
   using System;
   using System.Collections.Generic;
   using System.Linq;

   using NameSorter.Model;
   using NameSorter.Model.Implementation;

   /// <summary>
   /// Build for building an INameSorter object
   /// </summary>
   /// <remarks>
   /// After configure a NameSorterBuilder, call <c>Build</c> method
   /// to create a new INameSorter object.
   /// </remarks>
   public class NameSorterBuilder
   {
      private INameSource _source;
      private ISortAlgorithm _algorithm;
      private readonly List<INameDestination> _destinations
         = new List<INameDestination>();

      /// <summary>
      /// Configure an INameSource that read name list from
      /// a given file.
      /// </summary>
      /// <param name="fileName">File from which to read names</param>
      /// <returns>this NameSorterBuilder</returns>
      /// <exception cref="System.IO.FileNotFoundException">Specified file name not found</exception>
      /// <exception cref="System.IO.DirectoryNotFoundException">Specified path to file not found</exception>
      /// <exception cref="System.OutOfMemoryException">Not enought memory while reading file</exception>
      /// <exception cref="System.IO.IOException">error while reading file</exception>
      public NameSorterBuilder FromFile(string fileName) {
            if (fileName==null)
                throw new ArgumentNullException(nameof(fileName));
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException($"\"{nameof(fileName)}\" cannot be empty.");

         _source = new FileNameSource(fileName);
         return this;
      }
      /// <summary>
      /// Configure to build an INameSorter object using LinqSortAlgorithm
      /// </summary>
      /// <param name="desc">true to sort name descendingly, otherwise ascendingly</param>
      /// <returns>this NameSorterBuilder</returns>
      public NameSorterBuilder WithLinq(bool desc = false) {
         _algorithm = desc ? new LinqDescSortAlgorithm() as ISortAlgorithm
                    : new LinqAscSortAlgorithm() as ISortAlgorithm;
         return this;
      }
      /// <summary>
      /// Configure destinations for sorted name list
      /// </summary>
      /// <param name="destinations">an array of INameDestination objects</param>
      /// <returns>this NameSorterBuilder</returns>
      public NameSorterBuilder ToDestination(params INameDestination[] destinations) {
         if (destinations==null)
               throw new ArgumentNullException(nameof(destinations));

         _destinations.AddRange(destinations);
         return this;
      }
      public INameSorter Build() {
         return new SimpleNameSorter {
            Source = _source,
            Algorithm = _algorithm,
            Destinations = _destinations 
         };         
      } 
   }
}