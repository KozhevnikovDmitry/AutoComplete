using System;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;

namespace AutoComplete.Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public void Perfomance_Test()
        {
            // Arrange 
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var input = new StreamReader(File.OpenRead("test.in"));
            var output = new StringWriter();

            // Act
            var parsedInput = InputParser.Parse(input);

            var parseWatch = new Stopwatch();
            parseWatch.Start();
            var index = AutoComplete.BuildIndex(parsedInput.Item1, 10);
            Console.WriteLine("buildindex " + parseWatch.ElapsedMilliseconds);


            var completeWatch = new Stopwatch();
            completeWatch.Start();
            AutoComplete.WriteCompletions(index, parsedInput.Item2, output);
            Console.WriteLine("complete " + completeWatch.ElapsedMilliseconds);


            // Assert
            Console.WriteLine("RunTime " + parseWatch.ElapsedMilliseconds);
            Assert.That(parseWatch.Elapsed.TotalSeconds < 10);
        }

        [Test]
        public void Correctness_Test()
        {
            // Arrange
            var correctResult = File.ReadAllText("test.out");
            var input = new StreamReader(File.OpenRead("test.in"));
            var writer = new StringWriter();

            // Act
            var parsedInput = InputParser.Parse(input);
            AutoComplete.Complete(parsedInput.Item1, parsedInput.Item2, 10, writer);

            // Assert
            Assert.AreEqual(writer.ToString(), correctResult);
        }
    }
}
