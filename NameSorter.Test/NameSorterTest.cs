namespace NameSorter.Test
{
    using System;
    using NUnit.Framework;
    using Moq;

    [TestFixture]
    public class NameSorterTest 
    {
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
