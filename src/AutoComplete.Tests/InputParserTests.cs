using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace AutoComplete.Tests
{
    [TestFixture]
    public class InputParserTests
    {
        [Test]
        public void Parse_SimpleSuccess_Test()
        {
            // Arrange
            var lines = new[] { "2", "aaa 10", "bbb 10", "2", "x", "z" };
            var sb = new StringBuilder();
            foreach (var line in lines)
            {
                sb.AppendLine(line);
            }

            // Act
            var result = InputParser.Parse(new StringReader(sb.ToString()));

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
            var lines = new[] { "2", "aaa 10", "bbb 20", "2", "x", "z" };  
            var sb = new StringBuilder();
            foreach (var line in lines)
            {
                sb.AppendLine(line);
            }

            // Act
            var result = InputParser.Parse(new StringReader(sb.ToString()));

            // Assert
            Assert.AreEqual(result.Item1[0], "bbb");
            Assert.AreEqual(result.Item1[1], "aaa");
        }

        [Test]
        public void Parse_FrequencyDescendingOrder_ThanByWordAscending_Test()
        {
            // Arrange
            var lines = new[] { "3", "aaa 10", "ccc 20", "bbb 20", "2", "x", "z" };
             var sb = new StringBuilder();
            foreach (var line in lines)
            {
                sb.AppendLine(line);
            }

            // Act
            var result = InputParser.Parse(new StringReader(sb.ToString()));

            // Assert
            Assert.AreEqual(result.Item1[0], "bbb");
            Assert.AreEqual(result.Item1[1], "ccc");
            Assert.AreEqual(result.Item1[2], "aaa");
        }
    }
}
