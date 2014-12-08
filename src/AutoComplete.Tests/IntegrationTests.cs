using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AutoComplete.Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public void Control_Simple_Test()
        {
            // Arrange
            var lines = new[]
            {
                "5",
                "kare 10",
                "kanojo 20",
                "karetachi 1",
                "korosu 7",
                "sakura 3",
                "3",
                "k",
                "ka",
                "kar",
            };

            // Act
            var input = InputParser.Parse(lines);
            var completes = AutoComplete.Complete(input.Item1, input.Item2, 10);

            // Assert
            Assert.AreEqual(completes[0], "kanojo\r\nkare\r\nkorosu\r\nkaretachi\r\n");
            Assert.AreEqual(completes[1], "kanojo\r\nkare\r\nkaretachi\r\n");
            Assert.AreEqual(completes[2], "kare\r\nkaretachi\r\n");
        }

        [Test]
        public void Control_SmallInput_Test()
        {
            // Arrange
            var inputLines = File.ReadAllLines("smallTest.in");

            // Act
            var input = InputParser.Parse(inputLines);
            var completes = AutoComplete.Complete(input.Item1, input.Item2, 3);

            // Assert
            Assert.AreEqual(completes.Count, 10);

            Assert.AreEqual(completes[0], "aaa\r\naa\r\nabbc\r\n");
            Assert.AreEqual(completes[1], "bcc\r\nbccb\r\nbaaaba\r\n");
            Assert.AreEqual(completes[2], "cc\r\nccb\r\nccc\r\n");
            Assert.AreEqual(completes[3], "abbc\r\nabb\r\nabcbb\r\n");
            Assert.AreEqual(completes[4], "cba\r\n");
            Assert.AreEqual(completes[5], "bcc\r\nbccb\r\n");
            Assert.AreEqual(completes[6], "baaaba\r\n");
            Assert.AreEqual(completes[7], "aaa\r\naa\r\nabbc\r\n");
            Assert.AreEqual(completes[8], "bcc\r\nbccb\r\nbaaaba\r\n");
            Assert.AreEqual(completes[9], "cc\r\nccb\r\nccc\r\n");
        }

        [Test]
        public void Perfomance_Test()
        {
            // Arrange
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var inputLines = File.ReadAllLines("test.in");

            // Act
            var input = InputParser.Parse(inputLines);
            AutoComplete.Complete(input.Item1, input.Item2, 10);

            // Assert
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds/10);
            Console.WriteLine("RunTime " + elapsedTime);

            Assert.That(ts.TotalSeconds < 10);
        }

        [Test]
        public void Correctness_Test()
        {
            // Arrange
            var correctResult = File.ReadAllText("test.out");
            var inputLines = File.ReadAllLines("test.in");

            // Act
            var input = InputParser.Parse(inputLines);
            var completes = AutoComplete.Complete(input.Item1, input.Item2, 10);
            var resultToCheck = completes.Aggregate(string.Empty, (current, s) => current + s + "\r\n");

            // Assert
            Assert.AreEqual(correctResult, resultToCheck);
        }
    }
}
