namespace NameSorter.Test
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using Moq;
    using NameSorter.Model;
    using NameSorter.Model.Implementation;

    [TestFixture]
    public class LinqSortAlgorithmTest 
    {
        [Test]
        [Description("Test LinqSortAlgorithm")]
        public void TestLinqSort() {
            var sortAlgorithm = new LinqSortAlgorithm();
            var sortedNames = sortAlgorithm.Sort(TestDataHelper.Names);

            Assert.That(TestDataHelper.ExprectedSortedNames, Is.EqualTo(sortedNames));
        }
    }
}
