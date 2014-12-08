using System;
using System.Linq;
using NUnit.Framework;

namespace AutoComplete.Tests
{
    [TestFixture]
    public class InputParserTests
    {
        [Test]
        public void Parse_NullInput_Fails_Test()
        {
            // Arrange
            string[] input = null;

            // Assert
            Assert.Throws<ArgumentException>(() => InputParser.Parse(input));
        }

        [Test]
        public void Parse_EmptyInput_Fails_Test()
        {
            // Arrange
            var input = new string[0];

            // Assert
            Assert.Throws<ArgumentException>(() => InputParser.Parse(input));
        }

        [Test]
        public void Parse_WrongWordCountFormat_Fails_Test()
        {
            // Arrange
            var input = new[] { "blabla"};

            // Assert
            Assert.Throws<UnexpectedInputFormatException>(() => InputParser.Parse(input));
        }

        [Test]
        public void Parse_WordCountIsZero_Fails_Test()
        {
            // Arrange
            var input = new[] { "0" };

            // Assert
            Assert.Throws<UnexpectedInputFormatException>(() => InputParser.Parse(input));
        }

        [Test]
        public void Parse_WordCountIsNegative_Fails_Test()
        {
            // Arrange
            var input = new[] { "-1" };

            // Assert
            Assert.Throws<UnexpectedInputFormatException>(() => InputParser.Parse(input));
        }

        [Test]
        public void Parse_TooMuchWordCount_Fails_Test()
        {
            // Arrange
            var input = new[] { "10", "a 10", "1", "a" };

            // Assert
            Assert.Throws<InconsistentInputDataException>(() => InputParser.Parse(input));
        }

        [Test]
        public void Parse_WrongPrefixCountFormat_Fails_Test()
        {
            // Arrange
            var input = new[] { "1", "a 10", "blabla", "a" };

            // Assert
            Assert.Throws<UnexpectedInputFormatException>(() => InputParser.Parse(input));
        }
        [Test]
        public void Parse_PrefixCountIsZero_Fails_Test()
        {
            // Arrange
            var input = new[] { "1", "a 10", "0", "a" };

            // Assert
            Assert.Throws<UnexpectedInputFormatException>(() => InputParser.Parse(input));
        }

        [Test]
        public void Parse_PrefixIsNegative_Fails_Test()
        {
            // Arrange
            var input = new[] { "1", "a 10", "-1", "a" };

            // Assert
            Assert.Throws<UnexpectedInputFormatException>(() => InputParser.Parse(input));
        }

        [Test]
        public void Parse_TooMuchPrexixCount_Fails_Test()
        {
            // Arrange
            var input = new[] { "1", "a 10", "10", "a" };

            // Assert
            Assert.Throws<InconsistentInputDataException>(() => InputParser.Parse(input));
        }

        [TestCase("a")]
        [TestCase(" a 10")]
        [TestCase("10 a")]
        [TestCase("a a 10")]
        public void Parse_WrongVocabularyFormat_Fails_Test(string vocabularyLine)
        {
            // Arrange
            var input = new[] { "1", vocabularyLine, "1", "a" };

            // Assert
            Assert.Throws<UnexpectedInputFormatException>(() => InputParser.Parse(input));
        }

        [TestCase("bla")]
        [TestCase("-1")]
        public void Parse_WrongFrequencyFormat_Test(string frequencyStr)
        {
            // Arrange
            var input = new[] { "1", "a " + frequencyStr, "1", "a" };

            // Assert
            Assert.Throws<UnexpectedInputFormatException>(() => InputParser.Parse(input));
        }

        [Test]
        public void Parse_WordCountIsDiffer_Fails_Test()
        {
            // Arrange
            var input = new[] { "2", "a 10", "1", "a" };

            // Assert
            Assert.Throws<UnexpectedInputFormatException>(() => InputParser.Parse(input));
        }

        [Test]
        public void Parse_PrefixCountIsDiffer_Fails_Test()
        {
            // Arrange
            var input = new[] { "1", "a 10", "2", "a" };

            // Assert
            Assert.Throws<InconsistentInputDataException>(() => InputParser.Parse(input));
        }
        
        [Test]
        public void Parse_SimpleSuccess_Test()
        {
            // Arrange
            var input = new[] { "2", "aaa 10", "bbb 10", "2", "x", "z" };

            // Act
            var result = InputParser.Parse(input);

            // Assert
            Assert.AreEqual(result.Item1[0], "aaa");
            Assert.AreEqual(result.Item1[1], "bbb");
            Assert.AreEqual(result.Item2[0], "x");
            Assert.AreEqual(result.Item2[1], "z");
        }

        [Test]
        public void Parse_FrequencyDescendingOrder_Test()
        {
            // Arrange
            var input = new[] { "2", "aaa 10", "bbb 20", "2", "x", "z" };

            // Act
            var result = InputParser.Parse(input);

            // Assert
            Assert.AreEqual(result.Item1[0], "bbb");
            Assert.AreEqual(result.Item1[1], "aaa");
        }

        [Test]
        public void Parse_FrequencyDescendingOrder_ThanByWordAscending_Test()
        {
            // Arrange
            var input = new[] { "3", "aaa 10", "ccc 20", "bbb 20", "2", "x", "z" };

            // Act
            var result = InputParser.Parse(input);

            // Assert
            Assert.AreEqual(result.Item1[0], "bbb");
            Assert.AreEqual(result.Item1[1], "ccc");
            Assert.AreEqual(result.Item1[2], "aaa");
        }

        [Test]
        public void TakeTest()
        {
            var arr = new[] {1, 1, 2, 3, 4};

            var enm = arr.Take(10);

            Assert.AreEqual(enm.Count(), 5);
        }
    }
}
