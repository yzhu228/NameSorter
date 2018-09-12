namespace NameSorter.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
            var sortAlgorithm = new LinqAscSortAlgorithm();
            var sortedNames = sortAlgorithm.Sort(TestDataHelper.Names);
            Assert.That(sortedNames, Is.EqualTo(TestDataHelper.ExprectedSortedNames));

            sortedNames = sortAlgorithm.Sort(new List<string>());
            Assert.That(sortedNames, Is.EqualTo(new List<string>()));
        }

        [Test, Description("Test sorting descendingly")]
        public void TestLinqDescSort() {
            var sortAlgorithm = new LinqDescSortAlgorithm();
            var sortedNames = sortAlgorithm.Sort(TestDataHelper.Names);
            Assert.That(sortedNames, 
                Is.EqualTo(TestDataHelper.ExprectedSortedNames.Reverse()));

            sortedNames = sortAlgorithm.Sort(new List<string>());
            Assert.That(sortedNames, Is.EqualTo(new List<string>()));
        }
    }
}
