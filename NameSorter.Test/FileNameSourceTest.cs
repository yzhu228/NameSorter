namespace NameSorter.Test
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using NUnit.Framework;
    using Moq;
    using NameSorter.Model;
    using NameSorter.Model.Implementation;

    [TestFixture]
    public class FileNameSourceTest 
    {
        [Test, Description("Test reading a data file.")]
        [TestCase(@"./TestData/TestData.txt")]
        [TestCase(@"./TestData/TestData-Empty-Lines.txt")]
        public void ReadFromFileTest(string dataFile) {
            var source = new FileNameSource(dataFile);
            var names = source.GetNames();
            Assert.That(names, Is.EqualTo(TestDataHelper.Names));
        }

        [Test, Description("Test argument check")]
        public void TestArgumentCheck() {
            Assert.Throws<ArgumentNullException>(()=> new FileNameSource(null));
            Assert.Throws<ArgumentException>(() => new FileNameSource(" "));
            
            var source = new FileNameSource("aaa");
            Assert.Throws<FileNotFoundException>(() => source.GetNames());

            source = new FileNameSource(@"./aaa/datafile.txt");
            Assert.Throws<DirectoryNotFoundException>(() => source.GetNames());
        }
    }
}