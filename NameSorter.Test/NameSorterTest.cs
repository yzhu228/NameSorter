namespace NameSorter.Test
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using Moq;
    using NameSorter.Model;
    using NameSorter.Model.Implementation;
    
    [TestFixture]
    public class NameSorterTest 
    {
        private Mock<INameSource> _mockSource ;
        private Mock<ISortAlgorithm> _mockAlgorithm ;
        private Mock<INameDestination> _mockDestination1 ;
        private Mock<INameDestination> _mockDestination2 ;

        [SetUp]
        public void Init() {
           _mockSource = new Mock<INameSource>();
           _mockSource.Setup(s => s.GetNames()).Returns(TestDataHelper.Names);
           _mockAlgorithm = new Mock<ISortAlgorithm>();
           _mockAlgorithm.Setup(a => a.Sort(TestDataHelper.Names))
                .Returns(TestDataHelper.ExprectedSortedNames);
           _mockDestination1 = new Mock<INameDestination>();
           _mockDestination2 = new Mock<INameDestination>();
        }

        /// <summary>
        /// Test the basic relationship between INameSorter, INameSource, ISortAlgorithm
        /// and INameDestination. In a sentence, INameSorter uses ISortAlgorithm to sort 
        /// names provided by INameSource, the resulted name list is to be output to
        /// INameDestination.
        /// </summary>
        [Test]
        [Description("INameSorter dependency test")]
        public void TestSort() {
            var mockSorter = new Mock<INameSorter>();
            mockSorter.SetupProperty(s => s.Source, _mockSource.Object);
            mockSorter.SetupProperty(s => s.Algorithm, _mockAlgorithm.Object);
            mockSorter.SetupProperty(s => s.Destinations,
                new List<INameDestination> {_mockDestination1.Object, null, _mockDestination2.Object} 
            );
            mockSorter.Setup(s => s.Sort()).Callback(() => {
                var names = mockSorter.Object.Source.GetNames();
                var sortedNames = mockSorter.Object.Algorithm.Sort(names);
                foreach (var d in mockSorter.Object.Destinations) 
                    d?.OutputNames(sortedNames);
            });
            mockSorter.Object.Sort();
        }

        [Test, Description("Test SimpleNameSorter with mocks")]
        public void TestSimpleNameSorter() {
            var sorter = new SimpleNameSorter() {
                Source = _mockSource.Object,
                Algorithm = _mockAlgorithm.Object,
                Destinations = 
                    new List<INameDestination> {
                        _mockDestination1.Object, null, _mockDestination2.Object
                    } 
            };

            sorter.Sort();

            _mockSource.Verify(s => s.GetNames(), Times.Once);
            _mockAlgorithm.Verify(a => a.Sort(It.IsAny<IEnumerable<string>>()), Times.Once);
            _mockDestination1.Verify(d => d.OutputNames(It.IsAny<IEnumerable<string>>()), Times.Once);
            _mockDestination2.Verify(d => d.OutputNames(It.IsAny<IEnumerable<string>>()), Times.Once);
        }
    }
}
