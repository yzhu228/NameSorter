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
        private string[] _names = {
            "Janet Parsons",
            "Vaughn Lewis",
            "Adonis Julius Archer",
            "Shelby Nathan Yoder",
            "Marin Alvarez",
            "London Lindsey",
            "Beau Tristan Bentley",
            "Leo Gardner",
            "Hunter Uriah Mathew Clarke",
            "Mikayla Lopez",
            "Frankie Conner Ritter"
        };

        private IEnumerable<string> _exprectedSortedNames =
            new string[] {
                "Marin Alvarez",
                "Adonis Julius Archer",
                "Beau Tristan Bentley",
                "Hunter Uriah Mathew Clarke",
                "Leo Gardner",
                "Vaughn Lewis",
                "London Lindsey",
                "Mikayla Lopez",
                "Janet Parsons",
                "Frankie Conner Ritter",
                "Shelby Nathan Yoder"
            };

        [Test]
        [Description("Test LinqSortAlgorithm")]
        public void TestLinqSort() {
            var sortAlgorithm = new LinqSortAlgorithm();
            var sortedNames = sortAlgorithm.Sort(_names);

            Assert.That(_exprectedSortedNames, Is.EqualTo(sortedNames));
        }

    }
}
