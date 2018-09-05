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
    public class WriterNameDestinationTest
    {
        [Test, Description("Test TextWriter destination")]
        public void TestOutputNames() {
            var outputNames = new List<string>();

            var mockWriter = new Mock<TextWriter>();
            mockWriter.Setup(w => w.WriteLine(It.IsAny<string>()))
                .Callback<string>(s => outputNames.Add(s));

            var destination = new WriterNameDestination(mockWriter.Object);

            destination.OutputNames(TestDataHelper.ExprectedSortedNames);

            Assert.That(outputNames, Is.EqualTo(TestDataHelper.ExprectedSortedNames));
        }

        [Test, Description("Output names to console destination.")]
        public void TestConsoleDestination() {
            Console.SetOut(Console.Error);
            var destination = new ConsoleNameDestination();
            destination.OutputNames(TestDataHelper.ExprectedSortedNames);
        }

        [Test]
        public void TestFileDestination() {
            const string OutputFile = @"./sorted-data-file.txt";
            using (var destination = new FileNameDestination(OutputFile)) {
                destination.OutputNames(TestDataHelper.ExprectedSortedNames);
            }

            Assert.That(File.Exists(OutputFile));
        }

        [Test, Description("Test argument check")]
        public void TestArgumentCheck() {
            Assert.Throws<ArgumentNullException>(()=> new FileNameDestination(null));
            Assert.Throws<ArgumentException>(() => new FileNameDestination(" "));
        }

    }
}