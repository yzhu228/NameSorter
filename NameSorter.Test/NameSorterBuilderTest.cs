namespace NameSorter.Test
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using NUnit.Framework;
    using Moq;
    using NameSorter.Model;
    using NameSorter.Model.Implementation;

   [TestFixture]
   public class NameSorterBuliderTest
   {
      private readonly Mock<INameDestination> _mockDestination 
         = new Mock<INameDestination>();
      private readonly List<string> _sortedNames 
         = new List<string>();

      [SetUp]
      public void Init() {
         _sortedNames.Clear();
         _mockDestination.Setup(d => d.OutputNames(It.IsAny<IEnumerable<string>>()))
            .Callback((IEnumerable<string> ns) => _sortedNames.AddRange(ns));
      }

      [Test, Description("Wire up NameSorterBuilder, ascending")]
      public void TestAscSorterBuilder() {
         var builder = new NameSorterBuilder();
         using (var sorter = builder.FromFile("./TestData/TestData.txt")
                             .WithLinq()
                             .ToDestination(_mockDestination.Object, null)
                             .Build()) 
         {
            sorter.Sort();
         }
         Assert.That(_sortedNames, Is.EqualTo(TestDataHelper.ExprectedSortedNames));
      }

      [Test, Description("Wire up NameSorterBuilder, descending")]
      public void TestDescSorterBuilder() {
         var builder = new NameSorterBuilder();
         using (var sorter = builder.FromFile("./TestData/TestData.txt")
                             .WithLinq(true)
                             .ToDestination(_mockDestination.Object, null)
                             .Build()) 
         {
            sorter.Sort();
         }
         Assert.That(_sortedNames, 
            Is.EqualTo(TestDataHelper.ExprectedSortedNames.Reverse()));

      }

      [Test, Description("Test argument validation process")]
      public void TestArgumentValidation() {
         var builder = new NameSorterBuilder();

         Assert.Throws<ArgumentNullException>(() => builder.ToDestination(null));
         Assert.Throws<ArgumentException>(() => builder.FromFile(""));
         Assert.Throws<ArgumentNullException>(() => builder.FromFile(null));

         Assert.DoesNotThrow(()=>builder.ToDestination());
         Assert.DoesNotThrow(()=>builder.ToDestination(new INameDestination[0]));
         Assert.DoesNotThrow(()=>builder.ToDestination(null, new ConsoleNameDestination(), null));
      }
   }
}