namespace NameSorter
{
   using System;
   using System.Collections.Generic;
   using System.Linq;

   using NameSorter.Model;
   using NameSorter.Model.Implementation;

   public class NameSorterBuilder
   {
      private INameSource _source;
      private ISortAlgorithm _algorithm;
      private readonly List<INameDestination> _destinations
         = new List<INameDestination>();

      public NameSorterBuilder FromFile(string filename) {
         _source = new FileNameSource(filename);
         return this;
      }

      public NameSorterBuilder WithLinq(bool desc) {
         _algorithm = desc ? new LinqDescSortAlgorithm() as ISortAlgorithm
                    : new LinqAscSortAlgorithm() as ISortAlgorithm;
         return this;
      }
      public NameSorterBuilder ToDestination(params INameDestination[] destination) {
         _destinations.AddRange(destination);
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