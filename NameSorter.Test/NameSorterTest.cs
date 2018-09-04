namespace NameSorter.Test
{
    using System;
    using NUnit.Framework;
    using Moq;

    [TestFixture]
    public class NameSorterTest 
    {
        /// <summary>
        /// Test the basic relationship between INameSorter, INameSource, ISortAlgorithm
        /// and INameDestination. In a sentence, INameSorter uses ISortAlgorithm to sort 
        /// names provided by INameSource, the resulted name list is to be output to
        /// INameDestination.
        /// </summary>
        [Test]
        public void TestSort() {
           var mockSource = new Mock<INameSource>();
           var mockAlgorithm = new Mock<ISortAlgorithm>();
           var mockDestination = new Mock<INameDestination>();

           var mockSorter = new Mock<INameSorter>();

           mockSorter.Object.Sort(mockSource.Object, mockAlgorithm.Object, mockDestination.Object);
        }
    }
}
