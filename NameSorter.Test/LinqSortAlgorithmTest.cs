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
            var sortAlgorithm = new LinqSortAlgorithm(
                    ns => ns.OrderBy(n=>n.LastName).ThenBy(n=>n.GivenName)
                );
            var sortedNames = sortAlgorithm.Sort(TestDataHelper.Names);

            Assert.That(sortedNames, Is.EqualTo(TestDataHelper.ExprectedSortedNames));
        }

        [Test]
        public void TestLinqDescSort() {
            var sortAlgorithm = new LinqSortAlgorithm(
                    ns => ns.OrderByDescending(n=>n.LastName)
                            .ThenByDescending(n=>n.GivenName)
                );
            var sortedNames = sortAlgorithm.Sort(TestDataHelper.Names);

            Assert.That(sortedNames, 
                Is.EqualTo(TestDataHelper.ExprectedSortedNames.Reverse()));

        }
    }
}
